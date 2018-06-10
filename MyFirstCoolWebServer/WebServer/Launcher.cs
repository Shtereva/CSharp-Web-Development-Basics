using HTTPServer.GameStore.App;
using HTTPServer.Server;
using HTTPServer.Server.Routing;

namespace HTTPServer
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Run(args);
        }

        static void Run(string[] args)
        {
            // Initialize application
            var application = new GameStoreApp();
            application.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            // Configure App Route Configuration
            application.Configure(appRouteConfig);

            var server = new WebServer(8000, appRouteConfig);

            server.Run();
        }
    }
}
