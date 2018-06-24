namespace SimpleMvc.Framework.Views
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Contracts;
    public class View : IRenderable
    {
        private const string BaseLayoutFileName = "Layout";
        private const string ContentPlaceholder = "{{{content}}}";
        private const string HtmlExtension = ".html";
        private const string LocalErrorPath = @"\SimpleMvc.Framework\Errors\Error.html";

        private readonly string templateFullQualifiedName;

        private readonly IDictionary<string, string> viewData;

        public View(string templateFullQualifiedName, IDictionary<string, string> viewData)
        {
            this.templateFullQualifiedName = templateFullQualifiedName;
            this.viewData = viewData;
        }

        public string Render()
        {
            string fullHtml = this.ReadFile();

            if (this.viewData.Any())
            {
                foreach (var parameter in this.viewData)
                {
                    fullHtml = fullHtml.Replace($"{{{{{{{parameter.Key}}}}}}}", parameter.Value);
                }
            }

            return fullHtml;
        }

        private string ReadFile()
        {
            string layoutHtml = this.RenderLayoutHtml();
            string templateFullQualifiedNameWithExtension = this.templateFullQualifiedName + HtmlExtension;

            if (!File.Exists(templateFullQualifiedNameWithExtension))
            {
                string errorPath = this.GetErrorPath();
                string errorHtml = File.ReadAllText(errorPath);

                this.viewData["error"] = "Requested view does not exist!";
                return errorHtml;
            }

            string templateHtmlFileContent = File.ReadAllText(templateFullQualifiedNameWithExtension);

            layoutHtml = layoutHtml.Replace(ContentPlaceholder, templateHtmlFileContent);

            return layoutHtml;
        }

        private string GetErrorPath()
        {
            var appDirectoryPath = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(appDirectoryPath);
            var parentDirectoryPath = parentDirectory.FullName;

            string errorPagePath = parentDirectoryPath + LocalErrorPath;

            return errorPagePath;
        }

        private string RenderLayoutHtml()
        {
            string layoutHtmlQualifiedName = $@"{MvcContext.Get.ViewsFolder}\{BaseLayoutFileName}{HtmlExtension}";

            if (!File.Exists(layoutHtmlQualifiedName))
            {
                string errorPath = this.GetErrorPath();
                string errorHtml = File.ReadAllText(errorPath);

                this.viewData["error"] = "Layout view does not exist!";
                return errorHtml;
            }

            string layoutHtmlFileContent = File.ReadAllText(layoutHtmlQualifiedName);

            return layoutHtmlFileContent;
        }
    }
}
