namespace HTTPServer.GameStore.App.Controllers
{
    using HTTPServer.ByTheCakeApplication.ViewModels;
    using Server.Http;
    using ViewModels.Account;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class AccountController : BaseController
    {
        private const string RegisterPath = @"/account/register";
        private const string LoginPath = @"/account/login";
        private const string HomePath = @"/home/index";
        public AccountController(IHttpRequest req) : base(req)
        {
        }

        public IHttpResponse Register()
        {
            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse Register(RegisterViewModel viewModel)
        {
            if (!Validator.Email(viewModel.Email)
                && !Validator.FullName(viewModel.FullName)
                && !Validator.Password(viewModel.Password, viewModel.ConfirmPassword))
            {
                this.ViewData["showError"] = "block";
                this.ViewData["errorMessage"] = "Invalid Credentials.";
                return this.FileViewResponse(RegisterPath);
            }

            var success = this.userService.Create(viewModel.Email, viewModel.FullName, viewModel.Password);

            if (success)
            {
                return new RedirectResponse(LoginPath);
            }

            this.ViewData["showError"] = "block";
            this.ViewData["errorMessage"] = "User already exist.";
            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse LogIn()
        {
            return this.FileViewResponse(LoginPath);
        }

        public IHttpResponse LogIn(LoginViewModel viewModel)
        {
            var success = this.userService.Find(viewModel.Email, viewModel.Password);

            if (!success)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["errorMessage"] = "Invalid Credentials.";
                return this.FileViewResponse(LoginPath);
            }

            this.LoginCurrentUser(viewModel.Email);

            return new RedirectResponse(@"/");
        }

        public IHttpResponse LogOut()
        {
            this.Request.Session.Clear();
            return new RedirectResponse(@"/");
        }

        private void LoginCurrentUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
            this.Request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}
