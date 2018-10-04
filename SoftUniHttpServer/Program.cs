namespace SoftUniHttpServer
{
    
    public static class Program
    {
        public static void Main()
        {
            IHttpServer server = new HttpServer();

            server.Start();
        }
    }



   
}
