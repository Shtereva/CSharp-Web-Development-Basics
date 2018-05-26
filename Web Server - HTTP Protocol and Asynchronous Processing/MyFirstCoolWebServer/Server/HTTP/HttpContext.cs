using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest httpRequest;

        public HttpContext(string requestString)
        {
            CommonValidator.NullOrWhiteSpaceCheck(requestString, nameof(requestString));

            this.httpRequest = new HttpRequest(requestString);
        }

        public IHttpRequest HttpRequest => this.httpRequest;
    }
}