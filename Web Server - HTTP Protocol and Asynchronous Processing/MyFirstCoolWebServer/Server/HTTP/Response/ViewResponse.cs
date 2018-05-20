using System;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Exceptions;

namespace MyFirstCoolWebServer.Server.HTTP.Response
{
    public class ViewResponse : HttpResponse
    {
        private readonly IView view;
        public ViewResponse(ResponseStatusCode responseCode, IView view)
        {
            this.ValidateStatusCode(responseCode);

            this.view = view;
            this.StatusCode = responseCode;
        }

        private void ValidateStatusCode(ResponseStatusCode responseCode)
        {
            int statusCodeNumber = (int) responseCode;

            if (statusCodeNumber >= 300 && statusCodeNumber < 400)
            {
                throw new InvalidResponseException("View response need a status code below 300 above 400(inclusive).");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} {this.view.View()}";
        }
    }
}
