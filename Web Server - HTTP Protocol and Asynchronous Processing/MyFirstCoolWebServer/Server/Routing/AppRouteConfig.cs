using System;
using System.Collections.Generic;
using System.Linq;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Handlers;
using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Server.Routing
{
    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly IDictionary<RequestMethod, IDictionary<string, RequestHandler>> routes;
        public IReadOnlyDictionary<RequestMethod, IDictionary<string, RequestHandler>> Routes => 
            (IReadOnlyDictionary<RequestMethod, IDictionary<string, RequestHandler>>) this.routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<RequestMethod, IDictionary<string, RequestHandler>>();

            var aviableMethods = Enum
                .GetValues(typeof(RequestMethod))
                .Cast<RequestMethod>();

            foreach (var requestMethod in aviableMethods)
            {
                this.routes[requestMethod] = new Dictionary<string, RequestHandler>();
            }
        }
        public void AddRoute(string route, RequestHandler requestHandler)
        {
            var handlerName = requestHandler.GetType().Name.ToLower();

            if (handlerName.Contains("get"))
            {
                this.routes[RequestMethod.Get].Add(route, requestHandler);
            }

            else if (handlerName.Contains("post"))
            {
                this.routes[RequestMethod.Post].Add(route, requestHandler);
            }
            else
            {
                throw new InvalidOperationException("Invalid handler!");
            }
        }
    }
}
