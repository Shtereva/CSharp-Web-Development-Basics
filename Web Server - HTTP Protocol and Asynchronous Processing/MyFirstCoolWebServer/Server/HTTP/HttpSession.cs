using System.Collections.Generic;
using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.HTTP.Contracts;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> values;
        public string Id { get; private set; }

        public HttpSession(string id)
        {
            CommonValidator.NullOrWhiteSpaceCheck(id, nameof(id));

            this.Id = id;
            this.values = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));
            CommonValidator.NullCheck(value, nameof(value));

            this.values[key] = value;
        }

        public void Clear() => this.values.Clear();

        public object Get(string key)
        {
            CommonValidator.NullOrWhiteSpaceCheck(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                return null;
            }

            return this.values[key];
        }

        public T Get<T>(string key)
            => (T)this.Get(key);

        public bool Contains(string key) => this.values.ContainsKey(key);
    }
}
