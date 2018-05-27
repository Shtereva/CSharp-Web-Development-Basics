using MyFirstCoolWebServer.Application;
using MyFirstCoolWebServer.Server;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Routing;

namespace MyFirstCoolWebServer
{
    public class Launcher : IRunnable
    {
        public void Run()
        {
            var app = new ByTheCakeApplication();
            var routeConfig = new AppRouteConfig();
            app.Configure(routeConfig);

            var webServer = new WebServer(1337, routeConfig);
            webServer.Run();
        }
    }
}