namespace WebServer.Server.HTTP.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServer.Server.Enums;

    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        HttpHeaderCollection HeaderCollection { get; }

        string Path { get; }

        IDictionary<string, string> QueryParameters { get; }

        HttpRequestMethod RequestMethod { get; }

        string Url { get; }

        IDictionary<string, string> UrlParameters { get; }

        void AddUrlParameter(string key, string value);
    }
}
