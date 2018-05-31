using MyFirstCoolWebServer.Server.Enums;

namespace MyFirstCoolWebServer.Server.HTTP.Response
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
        {
            this.StatusCode = ResponseStatusCode.BadRequest;
        }
    }
}
