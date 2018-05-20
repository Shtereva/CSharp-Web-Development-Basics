using System.Collections.Generic;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Handlers;

namespace MyFirstCoolWebServer.Server.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<RequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        void AddRoute(string route, RequestHandler requestHandler);
    }
}
