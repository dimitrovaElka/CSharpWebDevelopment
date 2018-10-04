namespace SIS.HTTP.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using SIS.HTTP.Common;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Exceptions;
    using SIS.HTTP.Extensions;
    using SIS.HTTP.Headers;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLine)
        {
            return requestLine.Length == 3 && requestLine[2].ToLower() == GlobalConstants.HttpOneProtokolFragment.ToLower();
        }

        private bool IsValidRequestQueryString(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return false;
            }
            if (!queryString.Contains("="))
            {
                throw new BadRequestException();
            }
            return true;
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (!splitRequestContent.Any())
            {
                throw new BadRequestException();
            }
            string[] requestLine = splitRequestContent[0]
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
            
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseQueryParameters(requestBody, FormData);
        }

        private void ParseFormDataParameters(string requestBody)
        {
            return;
        }

        private void ParseHeaders(string[] requestContent)
        {
            if (!requestContent.Any())
            {
                throw new BadRequestException();
            }
            bool hasHost = false;
            int endIndex = Array.IndexOf(requestContent, string.Empty);
            for (int i = 0; i < endIndex; i++)
            {
                string[] headerArgs = requestContent[i]
                    .Split(new[] { ": " }, StringSplitOptions.None);

                //create a new HttpHeader and add it to the collection
                if (headerArgs.Length != 2)
                {
                    throw new BadRequestException();
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
                throw new BadRequestException();
            }
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];
 
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
            this.ParseRequestPath();

            if (!this.Url.Contains("?"))
            {
                return;
            }
            string queryWithFragment = this.Url.Split(new[] { "?" }, StringSplitOptions.RemoveEmptyEntries).Last();

            int indexOfFragment = queryWithFragment.IndexOf('#');
            string queryString = queryWithFragment;

            if (indexOfFragment > 0)
            {
                queryString = queryWithFragment.Substring(0, indexOfFragment);
            }

            this.ParseQueryParameters(queryString, this.QueryData);
        }

        private void ParseQueryParameters(string queryString, IDictionary<string, object> dict)
        {
            if (!IsValidRequestQueryString(queryString))
            {
                return;
            }

            var queryPairs = queryString.Split(new[] { '&' });
            foreach (var queryPair in queryPairs)
            {
                var queryArgs = queryPair.Split(new[] { '=' });
                if (queryArgs.Length != 2)
                {
                    throw new BadRequestException();
                }

                var queryKey = WebUtility.UrlDecode(queryArgs[0]);
                var queryValue = WebUtility.UrlDecode(queryArgs[1]);
                dict.Add(queryKey, queryValue);
            }
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            HttpRequestMethod parsedMethod;
            string method = requestLine[0];
            string capitalizedMethod = method.Capitalize();

            if (!Enum.TryParse(capitalizedMethod, true, out parsedMethod))
            {
                throw new BadRequestException();
            }

            this.RequestMethod = parsedMethod;
        }

    }
}
