using MyFirstCoolWebServer.Application.Controllers;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Handlers;
using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Application
{
    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute("/", new GetHandler(httpContext => new HomeController().Index()));

            appRouteConfig.AddRoute("/register", new GetHandler(httpContext => new UserController().RegisterGet()));

            appRouteConfig.AddRoute("/register"
                , new PostHandler(httpContext => new UserController().RegisterPost(httpContext.FormData["name"])));

            appRouteConfig.AddRoute("/user/{(?<name>[a-z]+)}",
                new GetHandler(httpContext => new UserController().Details(httpContext.UrlParameters["name"])));
        }
    }
}
