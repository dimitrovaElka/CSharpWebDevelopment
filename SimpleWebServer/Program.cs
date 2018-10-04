namespace SimpleWebServer
{
    using System;
    using System.IO;
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
                .Run(async () => await ConnectWithTcpClient(tcpListener))
                .GetAwaiter()
                .GetResult();
        }



        public static async Task ConnectWithTcpClient(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                var buffer = new byte[1024];
                var stream = client.GetStream();
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                var requestText = Encoding.UTF8.GetString(buffer);

                Console.WriteLine(new string('=', 60));
                Console.WriteLine(requestText);

                var responseText = File.ReadAllText("index.html");
                var responseBytes = Encoding.UTF8.GetBytes(
                     "HTTP/1.0 200 OK" + Environment.NewLine +
                     "Content-Type: text/plain" + Environment.NewLine +
                     "Content-Length: " + responseText.Length +
                     Environment.NewLine + Environment.NewLine + responseText
                    );

                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

                Console.WriteLine("Closing connection.");
                client.Dispose();
            }
        }

    }
}

