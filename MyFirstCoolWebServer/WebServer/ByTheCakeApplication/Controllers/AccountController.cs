namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using ViewModels;
    using ViewModels.Account;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using System;

    public class AccountController : Controller
    {
        private const string PathView = @"account\login";
        private readonly IUserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }
        public IHttpResponse Login()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(PathView);
        }

        public IHttpResponse Login(IHttpRequest req, LoginUserViewModel viewModel)
        {

            if (string.IsNullOrWhiteSpace(viewModel.Username)
                || string.IsNullOrWhiteSpace(viewModel.Password))
            {
                this.AddViewError("You have empty fields");

                return this.FileViewResponse(PathView);
            }

            var succes = this.userService.FindByUsername(viewModel.Username, viewModel.Password);

            if (succes)
            {
                this.LoginCurrentUser(req, viewModel.Username);

                return new RedirectResponse("/");
            }

            this.AddViewError("Invalid user credentials");

            return this.FileViewResponse(PathView);
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        public IHttpResponse Register()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(PathView);
        }

        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel viewModel)
        {
            this.SetDefaultViewData();

            if (string.IsNullOrWhiteSpace(viewModel.Username)
                || string.IsNullOrWhiteSpace(viewModel.Password)
                || string.IsNullOrWhiteSpace(viewModel.ConfirmPassword))
            {
                this.AddViewError("You have empty fields");

                return this.FileViewResponse(@"account\register");
            }

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                this.AddViewError("Passwords do not match");

                return this.FileViewResponse(PathView);
            }


            var success = this.userService.Create(viewModel.Username, viewModel.Password);

            if (success)
            {
                this.LoginCurrentUser(req, viewModel.Username);

                return new RedirectResponse("/");
            }

            this.AddViewError("This username is taken!");

            return this.FileViewResponse(PathView);
        }

        public IHttpResponse ViewProfile(IHttpRequest req)
        {
            var sessionKey = SessionStore.CurrentUserKey;

            if (!req.Session.Contains(sessionKey))
            {
                throw new InvalidOperationException("There is no logged in user.");
            }

            var username = req.Session.Get<string>(sessionKey);

            var user = this.userService.Profile(username);

            if (user == null)
            {
                throw new InvalidOperationException("The user could not be found");
            }
            this.ViewData["username"] = user.Username;
            this.ViewData["created"] = user.RegisteredOn;
            this.ViewData["orders"] = user.OrdersCount;

            return this.FileViewResponse(@"account\profile");
        }
        private void SetDefaultViewData() => this.ViewData["authDisplay"] = "none";

        private void LoginCurrentUser(IHttpRequest req, string username)
        {
            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}
