using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System.Text;

namespace MyFirstCoolWebServer.Server.HTTP.Response
{
    public abstract class HttpResponse : IHttpResponse
    {
        private string StatusMessage => this.StatusCode.ToString();
        public IHttpHeaderCollection HeaderCollection { get; }

        public IHttpCookieCollection Cookies { get; }
        public ResponseStatusCode StatusCode { get; protected set; }

        protected HttpResponse()
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }
        public override string ToString()
        {
            var response = new StringBuilder();
            int statusCodeNumber = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.StatusMessage}");
            response.AppendLine(this.HeaderCollection.ToString());
            response.AppendLine();

            return response.ToString();
        }
    }
}