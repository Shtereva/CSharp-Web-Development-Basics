using MyFirstCoolWebServer.Server.Enums;

namespace MyFirstCoolWebServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        IHttpHeaderCollection HeaderCollection { get; }

        IHttpCookieCollection Cookies { get; }

        ResponseStatusCode StatusCode { get; }
    }
}