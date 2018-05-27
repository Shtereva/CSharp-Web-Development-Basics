using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Handlers;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstCoolWebServer.Server.Routing
{
    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<RequestMethod, IDictionary<string, RequestHandler>> routes;
        public IReadOnlyDictionary<RequestMethod, IDictionary<string, RequestHandler>> Routes => this.routes;

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

        public void Get(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, RequestMethod.Get, new RequestHandler(handler));
        }

        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, RequestMethod.Post, new RequestHandler(handler));
        }

        public void AddRoute(string route, RequestMethod method, RequestHandler handler)
        {
            this.routes[method].Add(route, handler);
        }
    }
}