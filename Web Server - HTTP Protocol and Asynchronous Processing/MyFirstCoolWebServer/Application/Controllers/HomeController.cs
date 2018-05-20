using MyFirstCoolWebServer.Application.Views;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Response;

namespace MyFirstCoolWebServer.Application.Controllers
{
    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(ResponseStatusCode.Ok, new HomeIndexView());
        }
    }
}
