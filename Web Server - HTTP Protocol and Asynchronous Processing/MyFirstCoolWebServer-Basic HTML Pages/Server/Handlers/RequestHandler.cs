using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Handlers.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System;
using MyFirstCoolWebServer.Server.HTTP;

namespace MyFirstCoolWebServer.Server.Handlers
{
    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            CommonValidator.NullCheck(func, nameof(func));
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext request)
        {
            string sessionIdToSend = null;

            if (!request.Request.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var sessionId = Guid.NewGuid().ToString();

                request.Request.Session = SessionStore.Get(sessionId);

                sessionIdToSend = sessionId;
            }

            var response = this.func(request.Request);

            if (sessionIdToSend != null)
            {
                response.HeaderCollection.AddHeader(new HttpHeader(
                                                    HttpHeader.SetCookie,
                                                    $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/"));
            }

            if (!response.HeaderCollection.ContainsKey("text/html"))
            {
                response.HeaderCollection.AddHeader(new HttpHeader("Content-Type", "text/html"));
            }

            foreach (var cookie in response.Cookies)
            {
                response.HeaderCollection.AddHeader(new HttpHeader(HttpHeader.SetCookie, cookie.ToString()));
            }

            return response;
        }
    }
}