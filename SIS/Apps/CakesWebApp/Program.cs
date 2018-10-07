
namespace CakesWebApp
{
    using Controllers;
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Routing;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/register"] = request => new AccountController().Register(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/register"] = request => new AccountController().DoRegister(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/login"] = request => new AccountController().Login(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/login"] = request => new AccountController().DoLogin(request);

            var server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
