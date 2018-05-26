using MyFirstCoolWebServer.Server.Contracts;

namespace MyFirstCoolWebServer.Application.Views
{
    public class HomeIndexView : IView
    {
        public string View()
        {
            return "<body><h1>Welcome</h1></body>";
        }
    }
}