using MyFirstCoolWebServer.Server.Handlers.Contracts;
using System.Collections.Generic;

namespace MyFirstCoolWebServer.Server.Routing.Contracts
{
    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        IRequestHandler RequestHandler { get; }
    }
}