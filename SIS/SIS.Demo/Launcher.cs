namespace SIS.Demo
{
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Routing;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index();

            var server = new Server(8000, serverRoutingTable);

            server.Run();

        }
    }
}
