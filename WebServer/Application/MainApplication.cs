namespace WebServer.Application
{
    using System;
    using Application.Controllers;
    using Server.Contracts;
    using Server.Handlers;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            var handler = new GetHandler(httpContext => new HomeController().Index());
            appRouteConfig.AddRoute("/", handler);
        }
    }
}
