using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Routing.Contracts;

namespace MyFirstCoolWebServer.Server.Routing
{
    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> routes;
        public IDictionary<RequestMethod, IDictionary<string, IRoutingContext>> Routes => this.routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<RequestMethod, IDictionary<string, IRoutingContext>>();

            var methods = Enum
                .GetValues(typeof(RequestMethod))
                .Cast<RequestMethod>();

            foreach (var requestMethod in methods)
            {
                this.routes[requestMethod] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var registeredRoute in appRouteConfig.Routes)
            {
                var routesWithHandlers = registeredRoute.Value;

                foreach (var routeWithHandler in routesWithHandlers)
                {
                    var route = routeWithHandler.Key;
                    var handler = routeWithHandler.Value;
                    var requestMethod = registeredRoute.Key;

                    var parameters = new List<string>();

                    var parsedRouteRegex = this.ParseRoute(route, parameters);

                    var routingContex = new RoutingContext(parameters, handler);

                    this.routes[requestMethod].Add(parsedRouteRegex, routingContex);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var parsedRegex = new StringBuilder();

            parsedRegex.Append("^");

            if (route == "/")
            {
                parsedRegex.Append($"{route}$");
                return parsedRegex.ToString();
            }

            parsedRegex.Append("/");
            var tokens = route.Split("/", StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(parameters, tokens, parsedRegex);
            return parsedRegex.ToString();
        }

        private void ParseTokens(List<string> parameters, string[] tokens, StringBuilder parsedRegex)
        {
            for (int index = 0; index < tokens.Length; index++)
            {
                string end = index == tokens.Length - 1 ? "$" : "/";

                if (!tokens[index].StartsWith("{") && !tokens[index].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[index]}{end}");
                    continue;   
                }

                string pattern = "<\\w+>";

                var regex = new Regex(pattern);
                var match = regex.Match(tokens[index]);

                if (!match.Success)
                {
                    throw new InvalidOperationException($"Route parameter in {tokens[index]} is not valid.");
                }

                string paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                parameters.Add(paramName);

                parsedRegex.Append($"{tokens[index].Substring(1, tokens[index].Length - 2)}{end}");
            }
        }
    }
}
