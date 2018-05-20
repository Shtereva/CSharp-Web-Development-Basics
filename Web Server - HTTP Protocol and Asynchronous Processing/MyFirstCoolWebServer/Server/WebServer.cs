using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Routing;
using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Server
{
    public class WebServer : IRunnable
    {
        private readonly int port;

        private readonly IServerRouteConfig serverRouteConfig;

        private readonly TcpListener tcpListener;

        private bool isRunning;

        public WebServer(int port, IAppRouteConfig appRouteConfig)
        {
            CommonValidator.NullCheck(appRouteConfig, nameof(appRouteConfig));

            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);

            this.serverRouteConfig = new ServerRouteConfig(appRouteConfig);
        }
        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{this.port}");

            Task.Run(this.ListenLoop).Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();

                var connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);

                await connectionHandler.ProcessRequestAsync();
            }
        }
    }
}
