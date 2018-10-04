namespace SIS.HTTP.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Common;
    using Cookies;
    using Enums;
    using Exceptions;
    using Extensions;
    using Headers;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; set; }

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
            this.ParseRequestPath();

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());

            this.ParseCookies();
            bool requestHasBody = splitRequestContent.Length > 1;
            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1], requestHasBody);

        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsHeader("Cookie"))
            {
                var headerCookie = this.Headers.GetHeader("Cookie");
                HttpCookie cookie = new HttpCookie(headerCookie.Key, headerCookie.Value);
                Cookies.Add(cookie);
            }
        }

        private void ParseRequestParameters(string requestBody, bool requestHasBody)
        {
            this.ParseQueryParameters();
            if (requestHasBody)
            {
                ParseFormDataParameters(requestBody);
            }
        }

        private void ParseFormDataParameters(string requestBody)
        {
            FillData(requestBody, this.FormData);
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
            string url = requestLine[1];
            if (string.IsNullOrEmpty(url))
            {
                throw new BadRequestException();
            }

            this.Url = url;
        }

        private void ParseQueryParameters()
        {
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

            if (IsValidRequestQueryString(queryString))
            {
                this.FillData(queryString, this.QueryData);
            }
        }

        private void FillData(string dataString, Dictionary<string, object> dict)
        {
            if (!IsValidRequestQueryString(dataString))
            {
                return;
            }

            var dataPairs = dataString.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var dataPair in dataPairs)
            {
                var dataArgs = dataPair.Split(new[] { '=' });
                if (dataArgs.Length != 2)
                {
                    throw new BadRequestException();
                }

                var dataKey = WebUtility.UrlDecode(dataArgs[0]);
                var dataValue = WebUtility.UrlDecode(dataArgs[1]);
                dict[dataKey] = dataValue;
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

