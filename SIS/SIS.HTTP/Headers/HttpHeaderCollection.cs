namespace SIS.HTTP.Headers
{
    using SIS.HTTP.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header.Key, nameof(header.Key));
            CoreValidator.ThrowIfNull(header.Value, nameof(header.Value));

            var headerKey = header.Key;

            //if (!this.headers.ContainsKey(headerKey))
            //{
            //    this.headers[headerKey] = new List<HttpHeader>();
            //}

            this.headers[headerKey] = header;
        }

        public bool ContainsHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.headers.ContainsKey(key))
            {
                return null;
            }

            var result = this.headers[key];
            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                result.AppendLine($"{header.Key}: {header.Value}");
            }

            return result.ToString();
        }
    }
}
