using System;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.Handlers
{
    public class PostHandler : RequestHandler
    {
        public PostHandler(Func<IHttpRequest, IHttpResponse> func)
            : base(func)
        {
        }
    }
}
