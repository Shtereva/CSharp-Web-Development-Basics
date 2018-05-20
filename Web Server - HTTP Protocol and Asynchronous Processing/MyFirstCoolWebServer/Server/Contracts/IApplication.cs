using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Server.Contracts
{
    public interface IApplication
    {
        void Configure(IAppRouteConfig appRouteConfig);
    }
}
