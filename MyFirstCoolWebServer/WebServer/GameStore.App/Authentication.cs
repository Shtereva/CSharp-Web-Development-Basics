namespace HTTPServer.GameStore.App
{
    public class Authentication
    {
        public bool IsAuthenticated { get; private set; }
        public bool IsAdmin { get; private set; }

        public Authentication(bool isAuthenticated, bool isAdmin)
        {
            this.IsAuthenticated = isAuthenticated;
            this.IsAdmin = isAdmin;
        }
    }
}
