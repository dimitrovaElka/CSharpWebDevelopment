namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;
    using Enums;
    using Handlers;
    using System.Linq;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();
            var availableMethods = Enum.GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();
            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();
            }
         
        }
        public IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes => this.routes;

        public void AddRoute(string route, RequestHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
