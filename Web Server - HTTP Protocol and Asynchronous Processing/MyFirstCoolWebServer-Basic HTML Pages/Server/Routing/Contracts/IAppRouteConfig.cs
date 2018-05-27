using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Handlers;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;

namespace MyFirstCoolWebServer.Server.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<RequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        void Get(string route, Func<IHttpRequest, IHttpResponse> handler);

        void Post(string route, Func<IHttpRequest, IHttpResponse> handler);

        void AddRoute(string route, RequestMethod method, RequestHandler handler);
    }
}