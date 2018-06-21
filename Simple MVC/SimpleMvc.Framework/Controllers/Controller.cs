namespace SimpleMvc.Framework.Controllers
{
    using Contracts.Generic;
    using ViewEngine.Generic;
    using System.Runtime.CompilerServices;
    using Contracts;
    using ViewEngine;

    public abstract class Controller
    {
        protected IActionResult View([CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ViewsFolder}.{controllerName}.{caller}, {MvcContext.Get.AssemblyName}";

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult View(string controller, string action)
        {
            string fullQualifiedName = $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ViewsFolder}.{controller}.{action}, {MvcContext.Get.AssemblyName}";

            return new ActionResult(fullQualifiedName);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ViewsFolder}.{controllerName}.{caller}, {MvcContext.Get.AssemblyName}";

            return new ActionResult<TModel>(fullQualifiedName, model);
        }

        protected IActionResult<TModel> View<TModel>(string controller, string action, TModel model)
        {
            string fullQualifiedName = $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ViewsFolder}.{controller}.{action}, {MvcContext.Get.AssemblyName}";

            return new ActionResult<TModel>(fullQualifiedName, model);
        }
    }
}
