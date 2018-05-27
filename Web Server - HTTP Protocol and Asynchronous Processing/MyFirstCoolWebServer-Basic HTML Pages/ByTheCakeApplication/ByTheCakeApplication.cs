using MyFirstCoolWebServer.Application.Controllers;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Routing.Contracts;
using MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Controllers;

namespace MyFirstCoolWebServer.Application
{
    public class ByTheCakeApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get("/", req => new HomeController().Index());

            appRouteConfig.Get("/about", req => new HomeController().About());

            appRouteConfig.Get("/add", req => new CakesController().Add());

            appRouteConfig.Post("/add", req => new CakesController().Add(req.FormData["name"], req.FormData["price"]));

            appRouteConfig.Get("/search", req => new CakesController().Search());
        }
    }
}