using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _00.SympleWebServer
{
    public class StartUp
    {
        public static void Main()
        {
            int port = 1300;

            var ipAddress = IPAddress.Parse("127.0.0.1");

            var tcpListener = new TcpListener(ipAddress, port);

            tcpListener.Start();

            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            Task.Run(async () =>
            {
                await ConnectWithTcpClient(tcpListener);

            })
                .GetAwaiter()
                .GetResult();
        }

        private static async Task ConnectWithTcpClient(TcpListener tcpListener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client....");
                var client = await tcpListener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                byte[] buffer = new byte[1024];

                client.GetStream().Read(buffer, 0, buffer.Length);

                var message = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(message.Trim('\0'));

                byte[] data = Encoding.UTF8.GetBytes("Hello from my server!");
                client.GetStream().Write(data, 0, data.Length);

                Console.WriteLine("Closing connection.");
                client.GetStream().Dispose();
            }
        }
    }
}
