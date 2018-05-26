using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Handlers.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System;

namespace MyFirstCoolWebServer.Server.Handlers
{
    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            CommonValidator.NullCheck(func, nameof(func));
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext request)
        {
            var response = this.func(request.HttpRequest);

            if (!response.HeaderCollection.ContainsKey("text/html"))
            {
                response.AddHeader("Content-Type", "text/html");
            }

            return response;
        }
    }
}