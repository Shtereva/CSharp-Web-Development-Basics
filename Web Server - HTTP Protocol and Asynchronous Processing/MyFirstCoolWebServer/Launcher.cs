using MyFirstCoolWebServer.ByTheCakeApplication;
using MyFirstCoolWebServer.Server;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Routing;

namespace MyFirstCoolWebServer
{
    public class Launcher : IRunnable
    {
        public void Run()
        {
            var app = new ByTheCakeApp();
            var routeConfig = new AppRouteConfig();
            app.Configure(routeConfig);

            var webServer = new WebServer(1337, routeConfig);
            webServer.Run();
        }
    }
}