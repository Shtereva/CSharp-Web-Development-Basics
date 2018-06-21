namespace SimpleMvc.Framework.Contracts.Generic
{
    public interface IActionResult<TModel> : IInvokable
    {
        IRenderable<TModel> Action { get; set; }
    }
}
