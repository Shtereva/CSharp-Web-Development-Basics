using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void AddHeader(HttpHeader header)
        {
            CommonValidator.NullCheck(header, nameof(header));

            var headerKey = header.Key;

            if (!this.headers.ContainsKey(headerKey))
            {
                this.headers[headerKey] = new List<HttpHeader>();
            }

            this.headers[headerKey].Add(header);
        }

        public bool ContainsKey(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public ICollection<HttpHeader> GetHeader(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            if (!this.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the given collection.");
            }

            return this.headers[key];
        }

        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.headers.Values.GetEnumerator();
        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                var headerKey = header.Key;

                foreach (var headerValue in header.Value)
                {
                    result.AppendLine($"{headerKey}: {headerValue.Value}");
                }
            }

            return result.ToString().Trim();
        }
    }
}