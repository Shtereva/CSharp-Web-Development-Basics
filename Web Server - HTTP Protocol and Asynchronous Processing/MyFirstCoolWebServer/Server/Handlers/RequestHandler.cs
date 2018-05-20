using System;
using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Handlers.Contracts;
using MyFirstCoolWebServer.Server.HTTP;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.Handlers
{
    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            CommonValidator.NullCheck(func, nameof(func));
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext request)
        {
            var response = this.func(request.HttpRequest);

            response.AddHeader("Content-Type", "text/plain");

            return response;
        }
    }
}
