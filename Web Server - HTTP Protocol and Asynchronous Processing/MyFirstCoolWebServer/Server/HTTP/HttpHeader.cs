using System;
using MyFirstCoolWebServer.Server.Common;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpHeader
    {
        public string Key { get; }
        public string Value { get; }

        public HttpHeader(string key, string value)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));
            CommonValidator.NullOrWhiteSpaceCheck(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}
