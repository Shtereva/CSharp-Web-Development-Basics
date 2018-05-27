
using MyFirstCoolWebServer.Application.Views;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Response;
using MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication;

namespace MyFirstCoolWebServer.Application.Controllers
{
    public class UserController
    {
        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(ResponseStatusCode.Ok, new RegisterView());
        }

        public IHttpResponse RegisterPost(string name)
        {
            return new RedirectResponse($"/user/{name}");
        }

        public IHttpResponse Details(string name)
        {
            var model = new Model()
            {
                ["name"] = name
            };

            return new ViewResponse(ResponseStatusCode.Ok, new UserDetailsView(model));
        }
    }
}