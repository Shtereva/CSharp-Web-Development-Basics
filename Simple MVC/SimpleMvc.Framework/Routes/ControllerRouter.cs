namespace SimpleMvc.Framework.Routes
{
    using Attributes.Methods;
    using Controllers;
    using Contracts;
    using Helpers;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using WebServer.Contracts;
    using WebServer.Http.Contracts;
    using WebServer.Exceptions;
    using WebServer.Http.Response;
    using WebServer.Enums;
    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;

        private string requestMethod;

        private string controllerName;
        private string actionName;

        private object[] methodParams;
        public IHttpResponse Handle(IHttpRequest request)
        {
            this.getParams = new Dictionary<string, string>(request.UrlParameters);
            this.postParams = new Dictionary<string, string>(request.FormData);

            this.requestMethod = request.Method.ToString().ToUpper();

            this.RetrieveControllerAndActionNames(request);

            MethodInfo method = this.GetMethod();

            if (method == null)
            {
                return new NotFoundResponse();
            }

            var parametersInfo = method.GetParameters();

            this.methodParams = new object[parametersInfo.Length];

            this.SetMethodParameters(parametersInfo);

            var actionResult = (IInvokable) method.Invoke(this.GetController(), this.methodParams);

            string content = actionResult.Invoke();

            return new ContentResponse(HttpStatusCode.Ok, content);
        }

        private void SetMethodParameters(ParameterInfo[] parametersInfo)
        {
            int index = 0;

            foreach (var parameterInfo in parametersInfo)
            {
                if (parameterInfo.ParameterType.IsPrimitive || parameterInfo.ParameterType == typeof(string))
                {
                    object value = this.getParams[parameterInfo.Name];
                    this.methodParams[index] = Convert.ChangeType(value, parameterInfo.ParameterType);
                    index++;
                }
                else
                {
                    var bindingModelType = parameterInfo.ParameterType;

                    object bindingModel = Activator.CreateInstance(bindingModelType);

                    var properties = bindingModelType.GetProperties();

                    foreach (var propertyInfo in properties)
                    {
                        propertyInfo.SetValue(bindingModel,
                            Convert.ChangeType(
                                this.postParams[propertyInfo.Name],
                                propertyInfo.PropertyType)
                            );
                    }

                    this.methodParams[index] = Convert.ChangeType(bindingModel, bindingModelType);
                    index++;
                }
            }
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (var methodInfo in this.GetSuitableMethods())
            {
                var attributes = methodInfo
                    .GetCustomAttributes<HttpMethodAttribute>()
                    .ToArray();

                if (!attributes.Any() && this.requestMethod == "GET")
                {
                    return methodInfo;
                }

                foreach (var attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();

            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return controller
                .GetType()
                .GetMethods()
                .Where(m => m.Name == this.actionName);
        }

        private Controller GetController()
        {
            var controllerFullQualifiedName = $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ControllersFolder}.{this.controllerName}, {MvcContext.Get.AssemblyName}";

            var type = Type.GetType(controllerFullQualifiedName);

            if (type == null)
            {
                return null;
            }

            return (Controller)Activator.CreateInstance(type);
        }

        private void RetrieveControllerAndActionNames(IHttpRequest request)
        {
            var tokens = request.Path
                .Split("/?".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < 2)
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.controllerName = tokens[0].Capitalize() + MvcContext.Get.ControllersSuffix;
            this.actionName = tokens[1].Capitalize();
        }
    }
}
