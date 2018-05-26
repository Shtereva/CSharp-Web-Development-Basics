using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            CommonValidator.NullCheck(header, nameof(header));

            this.headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            if (!this.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the given collection.");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers.Values);
        }
    }
}