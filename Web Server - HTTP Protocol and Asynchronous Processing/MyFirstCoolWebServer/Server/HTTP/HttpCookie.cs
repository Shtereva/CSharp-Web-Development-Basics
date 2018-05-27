using System;
using MyFirstCoolWebServer.Server.Common;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpCookie
    {
        public string Key { get; private set; }

        public string Value { get; private set; }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; private set; } = true;

        // expires is in days
        public HttpCookie(string key, string value, int expires = 3)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));
            CommonValidator.NullOrWhiteSpaceCheck(value, nameof(value));

            this.Key = key;
            this.Value = value;

            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public HttpCookie(string key, string value, bool isNew, int expires = 3)
            : this(key, value, expires)
        {
            this.IsNew = isNew;
        }

        public override string ToString()
            => $"{this.Key}={this.Value}; Expires={this.Expires.ToLongTimeString()}";
    }
}
