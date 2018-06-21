namespace SimpleMvc.Framework.ViewEngine
{
    using Contracts;
    using System;
    public class ActionResult : IActionResult
    {
        public IRenderable Action { get; set; }

        public ActionResult(string fullViewQualifiedName)
        {
            this.Action = (IRenderable) Activator.CreateInstance(Type.GetType(fullViewQualifiedName));
        }
        public string Invoke()
        {
            return this.Action.Render();
        }

        
    }
}
