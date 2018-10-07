namespace SIS.HTTP.Requests
{
    using Cookies;
    using Enums;
    using Headers;
    using Sessions.Contracts;
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }

        Dictionary<string, object> FormData { get; }

        Dictionary<string, object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }

        IHttpSession Session { get; set; }

        HttpRequestMethod RequestMethod { get; }

        //IDictionary<string, string> UrlParameters { get; }

        //void AddUrlParameter(string key, string value);
    }
}
