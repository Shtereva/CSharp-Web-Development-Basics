using System;
using System.Collections;
using System.Collections.Generic;
using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            CommonValidator.NullCheck(cookie, nameof(cookie));

            this.cookies[cookie.Key] = cookie;
        }

        public void Add(string key, string value)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));
            CommonValidator.NullOrWhiteSpaceCheck(value, nameof(value));

            this.Add(new HttpCookie(key, value));
        }

        public bool ContainsKey(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            return this.cookies.ContainsKey(key);
        }

        public IEnumerator<HttpCookie> GetEnumerator()
            => this.cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.cookies.Values.GetEnumerator();

        public HttpCookie Get(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            if (!this.cookies.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the cookies collection.");
            }

            return this.cookies[key];
        }
    }
}
