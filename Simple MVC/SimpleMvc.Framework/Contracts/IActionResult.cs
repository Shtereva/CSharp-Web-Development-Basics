namespace SimpleMvc.Framework.Contracts
{
    public interface IActionResult : IInvokable 
    {
        IRenderable Action { get; set; }
    }
}
