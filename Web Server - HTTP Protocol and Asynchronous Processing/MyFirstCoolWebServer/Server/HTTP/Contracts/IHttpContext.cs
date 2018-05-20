namespace MyFirstCoolWebServer.Server.HTTP.Contracts
{
    public interface IHttpContext
    {
        IHttpRequest HttpRequest { get; }
    }
}
