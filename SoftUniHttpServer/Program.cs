namespace SoftUniHttpServer
{
    using System;
    using System.Text;

    public static class Program
    {
        public static void Main()
        {
            IHttpServer server = new HttpServer();

            server.Start();
        }
    }



   
}
