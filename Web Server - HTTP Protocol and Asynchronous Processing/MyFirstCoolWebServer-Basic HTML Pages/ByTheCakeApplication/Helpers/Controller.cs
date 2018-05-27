using System;
using System.IO;
using MyFirstCoolWebServer.Application.Views;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Response;

namespace MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Helpers
{
    public abstract class Controller
    {
        private const string Path = @"../../../ByTheCakeApplication/Resources/{0}.html";
        private const string ContentPlaceholder = "{{{content}}}";

        private string layoutHtml = File.ReadAllText(string.Format(Path, "layout"));

        public IHttpResponse FileViewResponse(string filename)
        {
            var fileHtml = File.ReadAllText(string.Format(Path, filename));

            var result = this.layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return new ViewResponse(ResponseStatusCode.Ok, new FileView(result));
        }

        public IHttpResponse FileViewResponse(string filename, string content)
        {
            var fileHtml = File.ReadAllText(string.Format(Path, filename));
            fileHtml += content;

            var result = this.layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return new ViewResponse(ResponseStatusCode.Ok, new FileView(result));
        }
    }
}
