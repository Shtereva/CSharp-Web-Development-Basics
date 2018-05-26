using MyFirstCoolWebServer.Application.Controllers;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Application
{
    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get("/", req => new HomeController().Index());

            appRouteConfig.Get("/register", req => new UserController().RegisterGet());

            appRouteConfig.Post("/register"
                , req => new UserController().RegisterPost(req.FormData["name"]));

            appRouteConfig.Get("/user/{(?<name>[a-zA-Z]+)}",
                req => new UserController().Details(req.UrlParameters["name"]));
        }
    }
}