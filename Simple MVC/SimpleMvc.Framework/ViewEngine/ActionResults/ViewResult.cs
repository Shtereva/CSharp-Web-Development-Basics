namespace SimpleMvc.Framework.ViewEngine.ActionResults
{
    using Contracts;
    public class ViewResult : IViewable
    {
        public IRenderable View { get; set; }

        public ViewResult(IRenderable view)
        {
            this.View = view;
        }

        public string Invoke()
        {
            return this.View.Render();
        }

       
    }
}
