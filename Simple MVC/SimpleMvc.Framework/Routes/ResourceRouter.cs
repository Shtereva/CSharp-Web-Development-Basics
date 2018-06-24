namespace SimpleMvc.Framework.Routes
{
    using System;
    using System.IO;
    using System.Linq;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ResourceRouter : IHandleable
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            string filefullName = request.Path.Split("/").Last();
            string fileExtension = request.Path.Split(".").Last();

            IHttpResponse response = null;

            try
            {
                byte[] fileContent = this.ReadFileContent(filefullName, fileExtension);
                response = new FileResponse(HttpStatusCode.Found, fileContent);
            }
            catch 
            {
               return new NotFoundResponse();
            }

            return response;
        }

        private byte[] ReadFileContent(string filefullName, string fileExtension)
        {
            byte[] byteContent = File.ReadAllBytes($@"{MvcContext.Get.ResourcesFolder}\{fileExtension}\{filefullName}");

            return byteContent;
        }
    }
}
