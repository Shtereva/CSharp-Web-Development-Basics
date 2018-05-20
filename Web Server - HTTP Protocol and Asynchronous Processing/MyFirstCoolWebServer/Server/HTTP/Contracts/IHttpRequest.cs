using System.Collections.Generic;
using MyFirstCoolWebServer.Server.Enums;

namespace MyFirstCoolWebServer.Server.HTTP.Contracts
{
    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        IHttpHeaderCollection HeaderCollection { get; }

        string Path { get; }

        IDictionary<string, string> QueryParameters { get; }

        RequestMethod RequestMethod { get; }

        string Url { get; }

        IDictionary<string, string> UrlParameters { get; }

        void AddUrlParameter(string key, string value);
    }
}
