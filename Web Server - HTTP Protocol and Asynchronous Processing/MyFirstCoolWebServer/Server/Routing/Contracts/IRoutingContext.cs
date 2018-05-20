using System.Collections.Generic;
using MyFirstCoolWebServer.Server.Handlers.Contracts;

namespace MyFirstCoolWebServer.Server.Routing.Contracts
{
    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        IRequestHandler RequestHandler { get; }
    }
}
