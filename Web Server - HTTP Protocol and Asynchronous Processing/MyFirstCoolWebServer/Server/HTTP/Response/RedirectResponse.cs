using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Enums;

namespace MyFirstCoolWebServer.Server.HTTP.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
        {
            CommonValidator.NullOrWhiteSpaceCheck(redirectUrl, nameof(redirectUrl));

            this.StatusCode = ResponseStatusCode.Found;

            this.AddHeader("Location", redirectUrl);
        }
    }
}