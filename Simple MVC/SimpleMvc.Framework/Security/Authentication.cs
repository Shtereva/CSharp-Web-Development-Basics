namespace SimpleMvc.Framework.Security
{
    public class Authentication
    {
        public string Name { get; }

        public bool IsAuthenticated { get; }

        internal Authentication()
        {
            this.IsAuthenticated = false;
        }

        internal Authentication(string name)
        {
            this.Name = name;
            this.IsAuthenticated = true;
        }
    }
}
