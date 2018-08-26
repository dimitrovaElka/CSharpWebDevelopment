namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;
    using Enums;
    using Handlers;
    using Exceptions;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestText)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestText);
        }

        public IDictionary<string, string> FormData { get; }

        public HttpHeaderCollection HeaderCollection { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            UrlParameters[key] = value;
        }

        private void ParseRequest(string requestText)
        {
            string[] requestLines = requestText
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = requestLines[0].Trim()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#'}, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();
        }

        private void ParseParameters()
        {
            throw new NotImplementedException();
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
                var key = headerArgs[0];
                var value = headerArgs[1];
                CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
                CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

                HttpHeader header = new HttpHeader(key, value);
                if (key.ToLower() == "host")
                {
                    hasHost = true;
                }
                HeaderCollection.Add(header);
            }

            if (!hasHost)
            {
                throw new BadRequestException("Invalid header Host");
            }
        }

        private HttpRequestMethod ParseRequestMethod(string typeRequest)
        {
            return (HttpRequestMethod)Enum.Parse(typeof(HttpRequestMethod), typeRequest);
        }
    }
}
