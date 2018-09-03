namespace WebServer
{
    using Application;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
    {
        //private WebServer webServer;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var app = new MainApplication();
            var routeConfig = new AppRouteConfig();
            app.Start(routeConfig);

            var webServer = new WebServer(1337, routeConfig);
            webServer.Run();
        }
    }
}
