
namespace WebServer.Server.Routing.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HTTP;
    using HTTP.Contracts;
    using Enums;
    using WebServer.Server.Handlers;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes
        { get; }

        void AddRoute(string route, RequestHandler handler);
    }
}
