using MyFirstCoolWebServer.Server.Enums;
using System.Collections.Generic;

namespace MyFirstCoolWebServer.Server.Routing.Contracts
{
    public interface IServerRouteConfig
    {
        IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
    }
}