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
        public IHttpResponse Handle(IHttpRequest request)
        {
            var getParams = new Dictionary<string, string>(request.UrlParameters);
            var postParams = new Dictionary<string, string>(request.FormData);

            string requestMethod = request.Method.ToString().ToUpper();

            string controllerName = string.Empty;
            string actionName = string.Empty;

            this.RetrieveControllerAndActionNames(request, out controllerName, out actionName);

            var controller = this.GetController(controllerName);

            if (controller != null)
            {
                controller.Request = request;
                controller.InitializeController();
            }

            MethodInfo method = this.GetMethod(controller, requestMethod, actionName);

            if (method == null)
            {
                return new NotFoundResponse();
            }

            var parametersInfo = method.GetParameters();

            var methodParams = this.AddParameters(parametersInfo, getParams, postParams);

            try
            {
                IHttpResponse response = this.GetResponce(method, controller, methodParams);
                return response;
            }
            catch (Exception e)
            {
                return new InternalServerErrorResponse(e);
            }
        }

        private IHttpResponse GetResponce(MethodInfo method, Controller controller, object[] methodParams)
        {
            object actionResult = method.Invoke(controller, methodParams);
            IHttpResponse response = null;

            if (actionResult is IViewable)
            {
                string content = ((IViewable)actionResult).Invoke();

                response = new ContentResponse(HttpStatusCode.Ok, content);
            }
            else if (actionResult is IRedirectable)
            {
                string redirectUrl = ((IRedirectable)actionResult).Invoke();

                response = new RedirectResponse(redirectUrl);
            }

            return response;
        }

        private object[] AddParameters(ParameterInfo[] parametersInfo,
            Dictionary<string, string> getParams,
            Dictionary<string, string> postParams)
        {
            object[] methodParams = new object[parametersInfo.Length];

            int index = 0;

            foreach (var parameterInfo in parametersInfo)
            {
                if (parameterInfo.ParameterType.IsPrimitive
                    || parameterInfo.ParameterType == typeof(string))
                {
                    methodParams[index] = this.ProcessPrimitiveParameter(parameterInfo, getParams);
                    index++;
                }
                else
                {
                    methodParams[index] = this.ProcessComplexParameter(parameterInfo, postParams);
                    index++;
                }
            }

            return methodParams;
        }

        private object ProcessComplexParameter(ParameterInfo parameterInfo, Dictionary<string, string> postParams)
        {
            var bindingModelType = parameterInfo.ParameterType;

            object bindingModel = Activator.CreateInstance(bindingModelType);

            var properties = bindingModelType.GetProperties();

            foreach (var propertyInfo in properties)
            {
                propertyInfo.SetValue(bindingModel,
                    Convert.ChangeType(
                        postParams[propertyInfo.Name],
                        propertyInfo.PropertyType)
                    );
            }

            return Convert.ChangeType(bindingModel, bindingModelType);
        }

        private object ProcessPrimitiveParameter(ParameterInfo parameterInfo, Dictionary<string, string> getParams)
        {
            object value = getParams[parameterInfo.Name];
            return Convert.ChangeType(value, parameterInfo.ParameterType);
        }

        private MethodInfo GetMethod(Controller controller, string requestMethod, string actionName)
        {
            foreach (var methodInfo in this.GetSuitableMethods(controller, actionName))
            {
                var attributes = methodInfo
                    .GetCustomAttributes<HttpMethodAttribute>()
                    .ToArray();

                if (!attributes.Any() && requestMethod == "GET")
                {
                    return methodInfo;
                }

                foreach (var attribute in attributes)
                {
                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return null;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods(Controller controller, string actionName)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return controller
                .GetType()
                .GetMethods()
                .Where(m => m.Name == actionName);
        }

        private Controller GetController(string controllerName)
        {
            var controllerFullQualifiedName = $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ControllersFolder}.{controllerName}, {MvcContext.Get.AssemblyName}";

            var type = Type.GetType(controllerFullQualifiedName);

            if (type == null)
            {
                return null;
            }

            return (Controller)Activator.CreateInstance(type);
        }

        private void RetrieveControllerAndActionNames(IHttpRequest request, out string controllerName, out string actionName)
        {
            var tokens = request.Path
                .Split("/?".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < 2)
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            controllerName = tokens[0].Capitalize() + MvcContext.Get.ControllersSuffix;
            actionName = tokens[1].Capitalize();
        }
    }
}
