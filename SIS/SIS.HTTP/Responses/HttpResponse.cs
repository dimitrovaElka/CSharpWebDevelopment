﻿namespace SIS.HTTP.Responses
{
    using System;
    using System.Linq;
    using System.Text;
    using Common;
    using Cookies;
    using Enums;
    using Extensions;
    using Headers;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() {}

        public HttpResponse(HttpResponseStatusCode statusCode)
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.Add(header);
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString()).Concat(this.Content).ToArray();
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));
            this.Cookies.Add(cookie);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            //result.Append($"{GlobalConstants.HttpOneProtokolFragment} {this.StatusCode.GetResponseLine()}");
            //result.Append(Environment.NewLine);

            //result.AppendLine($"{this.Headers}");

            //if (this.Cookies.HasCookies())
            //{
            //    foreach (var httpCookie in this.Cookies)
            //    {
            //        result.AppendLine($"Set-Cookie: {httpCookie}");
            //    }
            //}


            //result.Append(Environment.NewLine);

            result
                .Append($"{GlobalConstants.HttpOneProtokolFragment} {this.StatusCode.GetResponseLine()}").Append(Environment.NewLine)
                .Append(this.Headers).Append(Environment.NewLine);

            if (this.Cookies.HasCookies())
            {
                foreach (var httpCookie in this.Cookies)
                {
                    result.Append($"Set-Cookie: {httpCookie}").Append(Environment.NewLine);
                }
            }

            result.Append(Environment.NewLine);

            return result.ToString();
        }
    }
}
