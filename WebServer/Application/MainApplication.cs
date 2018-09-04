namespace WebServer.Application
{
    using Application.Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            // var handler = new GetHandler(httpContext => new HomeController().Index());
            //appRouteConfig.AddRoute("/", handler);
            appRouteConfig.Get("/", req => new HomeController().Index());
        }
    }
}
