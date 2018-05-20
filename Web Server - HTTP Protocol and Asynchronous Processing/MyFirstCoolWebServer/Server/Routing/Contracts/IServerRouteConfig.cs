using System.Collections.Generic;
using MyFirstCoolWebServer.Server.Enums;

namespace MyFirstCoolWebServer.Server.Routing.Contracts
{
    public interface IServerRouteConfig
    {
        IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
    }
}
