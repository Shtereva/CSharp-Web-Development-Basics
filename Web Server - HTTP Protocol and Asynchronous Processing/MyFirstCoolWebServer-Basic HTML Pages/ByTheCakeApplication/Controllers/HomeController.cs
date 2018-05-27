using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Helpers;

namespace MyFirstCoolWebServer.Application.Controllers
{
    public class HomeController : Controller
    {
        public IHttpResponse Index() =>  this.FileViewResponse(@"Home\index");
        
        public IHttpResponse About() => this.FileViewResponse(@"Home\about");
    }
}