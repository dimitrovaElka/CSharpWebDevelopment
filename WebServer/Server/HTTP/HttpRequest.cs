namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;
    using Enums;
    using Exceptions;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));

            this.Headers = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestText);
        }

        public IDictionary<string, string> FormData { get; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestText)
        {
            string[] requestLines = requestText
                .Split(Environment.NewLine);

            if (!requestLines.Any())
            {
                throw new BadRequestException("Request is invalid!");
            }
            string[] requestLine = requestLines[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine.First());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
        }

        private void ParseFormData(string formDataLine)
        {
            if (this.RequestMethod == HttpRequestMethod.Post)
            {
                this.ParseQuery(formDataLine, this.FormData);
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }
            string query = this.Url.Split(new[] { "?" }, StringSplitOptions.RemoveEmptyEntries).Last();
            // this.ParseQuery(query, this.QueryParameters);  //??? UrlParameters
            
            this.ParseQuery(query, this.UrlParameters);
        }

        private void ParseQuery(string queryString, IDictionary<string, string> dict)
        {
            if (!queryString.Contains("="))
            {
                return;
            }
            var queryPairs = queryString.Split(new[] { '&' });
            foreach (var queryPair in queryPairs)
            {
                var queryArgs = queryPair.Split(new[] { '=' });
                if (queryArgs.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryArgs[0]);
                var queryValue = WebUtility.UrlDecode(queryArgs[1]);
                dict.Add(queryKey, queryValue);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            bool hasHost = false;
            int endIndex = Array.IndexOf(requestLines, string.Empty);
            for (int i = 1; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i]
                    .Split(new[] { ": " }, StringSplitOptions.None);

                //create a new HttpHeader and add it to the collection
                if (headerArgs.Length != 2)
                {
                    throw new BadRequestException("Invalid header arguments");
                }
                var key = headerArgs[0];
                var value = headerArgs[1].Trim();
                CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
                CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

                HttpHeader header = new HttpHeader(key, value);
                if (key.ToLower() == "host")
                {
                    hasHost = true;
                }
                Headers.Add(header);
            }

            if (!hasHost)
            {
                throw new BadRequestException("Invalid header Host");
            }
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            //try
            //{
            //    return (HttpRequestMethod)Enum.Parse(typeof(HttpRequestMethod), typeRequest, true);
            //}
            //catch (Exception)
            //{
            //    throw new BadRequestException("Invalid type request method");
            //}
            HttpRequestMethod parsedMethod;
            if (!Enum.TryParse(method, true, out parsedMethod))
            {
                throw new BadRequestException("Invalid type request method");
            }

            return parsedMethod;
        }
    }
}
