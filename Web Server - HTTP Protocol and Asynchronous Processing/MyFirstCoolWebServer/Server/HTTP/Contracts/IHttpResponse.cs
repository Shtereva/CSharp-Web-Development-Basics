using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Enums;

namespace MyFirstCoolWebServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        IHttpHeaderCollection HeaderCollection { get; }

        ResponseStatusCode StatusCode { get; }

        void AddHeader(string key, string value);
    }
}
