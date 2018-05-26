using MyFirstCoolWebServer.Server.Common;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.Exceptions;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MyFirstCoolWebServer.Server.HTTP
{
    public class HttpRequest : IHttpRequest
    {
        private const string BadRequestMessage = "Invalid request";
        public IDictionary<string, string> FormData { get; private set; }
        public IHttpHeaderCollection HeaderCollection { get; }
        public string Path { get; private set; }
        public IDictionary<string, string> QueryParameters { get; private set; }
        public RequestMethod RequestMethod { get; private set; }
        public string Url { get; private set; }
        public IDictionary<string, string> UrlParameters { get; private set; }

        public HttpRequest(string requestString)
        {
            CommonValidator.NullOrWhiteSpaceCheck(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, string>();
            this.HeaderCollection = new HttpHeaderCollection();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public void AddUrlParameter(string key, string value)
        {
            CommonValidator.NullOrWhiteSpaceCheck(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            var lines = requestString.Split(Environment.NewLine);

            var methodAndUrl = lines[0].Split();

            if (methodAndUrl.Length != 3
                || methodAndUrl[2].ToLower() != "http/1.1")
            {
                throw new BadRequestExeption(BadRequestMessage);
            }

            this.RequestMethod = this.ParseMethod(methodAndUrl[0]);
            this.Url = methodAndUrl[1];
            this.Path = this.ParsePath(this.Url);
            this.ParseHeaders(lines);
            this.ParseParameters();
            this.ParseFormData(lines);
        }

        private void ParseFormData(string[] lines)
        {
            if (this.RequestMethod == RequestMethod.Post)
            {
                var data = lines[lines.Length - 1];
                this.ParseQuery(data, this.FormData);
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }
            var query = this.Url.Split("?").Last();

            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }

            var queryInfo = query.Split("&");

            foreach (var info in queryInfo)
            {
                var args = info.Split("=");
                if (args.Length != 2)
                {
                    throw new BadRequestExeption(BadRequestMessage);
                }

                string key = WebUtility.UrlDecode(args[0]);
                string value = WebUtility.UrlDecode(args[1]);

                dict[key] = value;
            }
        }

        private void ParseHeaders(string[] lines)
        {
            int emptyLineIndex = Array.IndexOf(lines, string.Empty);

            for (int i = 1; i < emptyLineIndex; i++)
            {
                var headerInfo = lines[i].Split(": ", StringSplitOptions.RemoveEmptyEntries);

                CommonValidator.NullCheck(headerInfo, nameof(headerInfo));

                if (headerInfo.Length != 2)
                {
                    throw new BadRequestExeption(BadRequestMessage);
                }

                string key = headerInfo[0];
                string value = headerInfo[1].Trim();

                this.HeaderCollection.AddHeader(new HttpHeader(key, value));

                if (!this.HeaderCollection.ContainsKey("Host"))
                {
                    throw new BadRequestExeption(BadRequestMessage);
                }
            }
        }

        private string ParsePath(string path)
        {
            return path.Split("#?".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private RequestMethod ParseMethod(string method)
        {
            try
            {
                return Enum.Parse<RequestMethod>(method, true);
            }
            catch (Exception)
            {
                throw new BadRequestExeption(BadRequestMessage);
            }
        }
    }
}