﻿namespace HTTPServer.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    public abstract class Controller 
    {
        public const string DefaultPath = @"..\..\..\{0}\Resources\{1}.html";
        public const string ContentPlaceholder = "{{{content}}}";

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>
            {
                ["authDisplay"] = "block",
                ["showError"] = "none",
                ["guestDisplay"] = "none",
                ["adminDisplay"] = "none",
                ["userDisplay"] = "none"
            };
        }

        protected IDictionary<string, string> ViewData { get; private set; }

        protected abstract string ApplicationDirectory { get; }

        protected void AddViewError(string errorMessage)
        {
            this.ViewData["error"] = errorMessage;
            this.ViewData["showError"] = "block";
        }
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
            
            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        private string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, "layout"));

            var fileHtml = File
                .ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }
    }
}
