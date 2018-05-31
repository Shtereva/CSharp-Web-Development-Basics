using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using MyFirstCoolWebServer.ByTheCakeApplication.Views;
using MyFirstCoolWebServer.Server.Enums;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Response;

namespace MyFirstCoolWebServer.ByTheCakeApplication.Infrastructure
{

    public abstract class Controller 
    {
        public const string DefaultPath = @"..\..\..\ByTheCakeApplication\Resources\{0}.html";
        public const string ContentPlaceholder = "{{{content}}}";

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>
            {
                ["authDisplay"] = "block"
            };
        }

        protected IDictionary<string, string> ViewData { get; private set; }
        
        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }
            
            return new ViewResponse(ResponseStatusCode.Ok, new FileView(result));
        }

        private string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File
                .ReadAllText(string.Format(DefaultPath, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }
    }
}
