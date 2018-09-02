namespace WebServer
{
    using Server.Routing;
    using Server;
    using Server.Contracts;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var routeConfig = new AppRouteConfig();
            this.webServer = new WebServer(8230, routeConfig);
            this.webServer.Run();
        }
    }
}
