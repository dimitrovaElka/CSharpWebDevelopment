namespace WebServer.Server
{

    using Common;
    using Handlers;
    using HTTP;
    using HTTP.Contracts;
    using Routing.Contracts;
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            var request = await this.ReadRequest();

            var context = new HttpContext(request);

            var response = new HttpHandler(this.serverRouteConfig).Handle(context);

            ArraySegment<byte> toBytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response.ToString()));

            await this.client.SendAsync(toBytes, SocketFlags.None);

            Console.WriteLine("-----REQUEST----");
            Console.WriteLine(request);
            Console.WriteLine("------RESPONSE----");
            Console.WriteLine(response);

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            var result = new StringBuilder();
            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

            int numBytesRead;

            while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                result.Append(Encoding.UTF8.GetString(data.Array, 0, numBytesRead));
                if (numBytesRead < 1023)
                {
                    break;
                }
            }
            var request = result.ToString();
            return request;
        }
    }
}
