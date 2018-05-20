using MyFirstCoolWebServer.Server.HTTP;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.Handlers.Contracts
{
    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext request);
    }
}
