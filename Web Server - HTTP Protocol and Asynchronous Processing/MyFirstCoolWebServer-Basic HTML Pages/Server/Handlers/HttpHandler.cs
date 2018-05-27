using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Handlers.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Response;
using MyFirstCoolWebServer.Server.Routing.Contracts;
using System.Text.RegularExpressions;

namespace MyFirstCoolWebServer.Server.Handlers
{
    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            CommonValidator.NullCheck(serverRouteConfig, nameof(serverRouteConfig));

            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext request)
        {
            var requestMethod = request.Request.RequestMethod;

            foreach (var registeredRoute in this.serverRouteConfig.Routes[requestMethod])
            {
                var routePattern = registeredRoute.Key;

                var match = Regex.Match(request.Request.Path, routePattern);

                if (!match.Success)
                {
                    continue;
                }

                foreach (var parameter in registeredRoute.Value.Parameters)
                {
                    request.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return registeredRoute.Value.RequestHandler.Handle(request);
            }

            return new NotFoundResponse();
        }
    }
}