namespace SimpleMvc.Framework.ViewEngine.Generic
{
    using Contracts.Generic;
    using System;
    public class ActionResult<TModel> : IActionResult<TModel>
    {
        public IRenderable<TModel> Action { get; set; }

        public ActionResult(string fullViewQualifiedName, TModel model)
        {
            this.Action = (IRenderable<TModel>) Activator.CreateInstance(Type.GetType(fullViewQualifiedName));

            this.Action.Model = model;
        }
        public string Invoke()
        {
            return this.Action.Render();
        }

        
    }
}
