namespace SIS.HTTP.Requests
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }

        Dictionary<string, object> FormData { get; }

        Dictionary<string, object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

        //IDictionary<string, string> UrlParameters { get; }

        //void AddUrlParameter(string key, string value);
    }
}
