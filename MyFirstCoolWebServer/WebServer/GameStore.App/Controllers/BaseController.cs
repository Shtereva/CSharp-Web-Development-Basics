namespace HTTPServer.GameStore.App.Controllers
{
    using Services;
    using Services.Contracts;
    using Server.Http;
    using Server.Http.Contracts;
    using Infrastructure;

    public abstract class BaseController : Controller
    {
        protected IHttpRequest Request { get; private set; }

        protected Authentication Authentication { get; private set; }

        protected readonly IUserService userService;
        protected BaseController(IHttpRequest req)
        {
            this.Authentication = new Authentication(false, false);
            this.Request = req;
            this.userService = new UserService();
            this.ApplyAuthenticationData();
        }
        protected override string ApplicationDirectory => @"GameStore.App";

        private void ApplyAuthenticationData()
        {
            var guestDisplay = "flex";
            var adminDisplay = "none";
            var userDisplay = "none";
            var adminShow = "none";

            var isAuthenticated = this.Request.Session.Contains(SessionStore.CurrentUserKey);

            if (isAuthenticated)
            {
                guestDisplay = "none";

                var username = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

                var isAdmin = this.userService.IsAdmin(username);

                if (isAdmin)
                {
                    adminDisplay = "flex";
                    adminShow = "inline-block";
                }
                else
                {
                    userDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData[nameof(guestDisplay)] = guestDisplay;
            this.ViewData[nameof(adminDisplay)] = adminDisplay;
            this.ViewData[nameof(userDisplay)] = userDisplay;
            this.ViewData[nameof(adminShow)] = adminShow;
        }
    }
}
