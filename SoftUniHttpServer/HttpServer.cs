namespace SoftUniHttpServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;


    public class HttpServer : IHttpServer
    {
        private bool isWorking;

        private readonly TcpListener tcpListener;

        private readonly RequestProcessor requestProcessor;

        public HttpServer()
        {
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            this.requestProcessor = new RequestProcessor();

        }

        public void Start()
        {
            this.tcpListener.Start();
            this.isWorking = true;
            Console.WriteLine("Started.");

            while (this.isWorking)
            {
                var client = this.tcpListener.AcceptTcpClient();
                requestProcessor.ProcessClient(client);

                ////Task.Run(() =>
                ////{
                ////    return requestProcessor.ProcessClient(client);
                ////});
            }
        }

        public void Stop()
        {
            this.isWorking = false;
            this.tcpListener.Stop();
        }
    }
}
