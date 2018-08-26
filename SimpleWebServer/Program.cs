﻿namespace SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    public class Program
    {
        public static void Main()
        {
            int port = 1337;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");  //localhost
            TcpListener tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();
            Console.WriteLine("Server started");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");
            // Establish а connection and read request
            Task
                .Run(async () => await Connect(tcpListener))
                .GetAwaiter()
                .GetResult();
        }



        public static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();

                var buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                var clientMessage = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(clientMessage.Trim('\0'));

                var responseMessage = "HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello from server";
                var data = Encoding.UTF8.GetBytes(responseMessage, 0, responseMessage.Length);
                await client.GetStream().WriteAsync(data, 0, data.Length);

                client.Dispose();
            }
        }

    }
}

