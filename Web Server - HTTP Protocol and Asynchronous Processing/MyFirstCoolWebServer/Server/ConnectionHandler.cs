using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Handlers;
using MyFirstCoolWebServer.Server.HTTP;
using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Server
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CommonValidator.NullCheck(client, nameof(client));
            CommonValidator.NullCheck(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            string request = await this.ReadRequest();

            var httpContext = new HttpContext(request);
            var response = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

            var toBytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(response.ToString()));

            await this.client.SendAsync(toBytes, SocketFlags.None);

            Console.WriteLine(request);
            Console.WriteLine(response);

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<string> ReadRequest()
        {
            var request = new StringBuilder();

            var data = new ArraySegment<byte>(new byte[1024]);

            int numBytesRead;

            while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                request.Append(Encoding.ASCII.GetString(data.Array, 0, numBytesRead));

                if (numBytesRead < 1024)
                {
                    break;
                }
            }

            return request.ToString();
        }
    }
}
