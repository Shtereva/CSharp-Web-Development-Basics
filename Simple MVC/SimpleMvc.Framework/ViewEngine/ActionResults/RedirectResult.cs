namespace SimpleMvc.Framework.ViewEngine.ActionResults
{
    using Contracts;
    public class RedirectResult : IRedirectable
    {
        public string RedirectUrl { get; }

        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string Invoke()
        {
            return this.RedirectUrl;
        }

        
    }
}
