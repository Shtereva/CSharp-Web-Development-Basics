using System;
using System.Net;
using System.Text;
using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.HTTP.Response
{
    public abstract class HttpResponse : IHttpResponse
    {

        private string StatusMessage => this.StatusCode.ToString();
        public IHttpHeaderCollection HeaderCollection { get; protected set; }
        public ResponseStatusCode StatusCode { get; protected set; }

        protected HttpResponse()
        {
            this.HeaderCollection = new HttpHeaderCollection();
        }

        public void AddHeader(string key, string value)
        {
            this.HeaderCollection.AddHeader(new HttpHeader(key, value));
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
