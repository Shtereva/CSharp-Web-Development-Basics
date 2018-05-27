using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Handlers.Contracts;
using MyFirstCoolWebServer.Server.Routing.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstCoolWebServer.Server.Routing
{
    public class RoutingContext : IRoutingContext
    {
        private List<string> parameters;
        public IEnumerable<string> Parameters => this.parameters;
        public IRequestHandler RequestHandler { get; }

        public RoutingContext(IEnumerable<string> parameters, IRequestHandler requestHandler)
        {
            CommonValidator.NullCheck(parameters, nameof(parameters));
            CommonValidator.NullCheck(requestHandler, nameof(requestHandler));

            this.parameters = parameters.ToList();
            this.RequestHandler = requestHandler;
        }
    }
}