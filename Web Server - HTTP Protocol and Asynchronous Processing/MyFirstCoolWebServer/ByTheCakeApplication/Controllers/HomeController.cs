using MyFirstCoolWebServer.ByTheCakeApplication.Infrastructure;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.ByTheCakeApplication.Controllers
{
    public class HomeController : Controller
    {
            public IHttpResponse Index() => this.FileViewResponse(@"home\index");

            public IHttpResponse About() => this.FileViewResponse(@"home\about");
    }
}