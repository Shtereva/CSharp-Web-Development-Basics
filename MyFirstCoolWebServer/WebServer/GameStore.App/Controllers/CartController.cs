namespace HTTPServer.GameStore.App.Controllers
{
    using Server.Enums;
    using Server.Http;
    using System.Linq;
    using System.Text;
    using ViewModels;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using Server.Http.Contracts;
    public class CartController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IUserService userService;

        public CartController(IHttpRequest req) : base(req)
        {
            this.gameService = new GameService();
            this.userService = new UserService();
        }

        // GET
        public IHttpResponse ShowCart()
        {
            if (this.Request.Method == HttpRequestMethod.Post)
            {
                return this.Order();
            }
            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);

            var games = this.gameService.GetCart(cart.Products);

            var sb = new StringBuilder();

            foreach (var cartViewModel in games)
            {
                var descriptionAsList = cartViewModel.Description.ToCharArray().Take(300).ToList();
                var indexOfLastDot = descriptionAsList.LastIndexOf('.');
                if (indexOfLastDot == -1)
                {
                    indexOfLastDot = 300;
                }

                var description = string.Join("", descriptionAsList.Take(indexOfLastDot + 1));

                var result = $@"<div class=""list-group-item"">
                    <div class=""media"">
                    <a class=""btn btn-outline-danger btn-lg align-self-center mr-3"" href=""/home/remove/{cartViewModel.GameId}"">X</a>
                    <img class=""d-flex mr-4 align-self-center img-thumbnail"" height=""127"" src=""{cartViewModel.Tumbnail}""
                    width=""227"" alt=""Generic placeholder image"">
                    <div class=""media-body align-self-center"">
                    <a href=""/admin/games/info/{cartViewModel.GameId}"">
                    <h4 class=""mb-1 list-group-item-heading""> {cartViewModel.Title} </h4>
                    </a>
                    <p>
                    {description}
                    </p>
                    </div>
                    <div class=""col-md-2 text-center align-self-center mr-auto"">
                    <h2> {cartViewModel.Price:f2}&euro; </h2>
                    </div>
                    </div>
                    </div>";

                sb.AppendLine(result);
            }

            var total = games.Sum(g => g.Price);

            this.ViewData["cart-games"] = sb.ToString();
            this.ViewData["total"] = total.ToString("F2");

            return this.FileViewResponse(@"/account/cart");
        }

        // GET
        public IHttpResponse Add(int gameId)
        {
            if (!this.gameService.Exist(gameId))
            {
                return new NotFoundResponse();
            }

            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);

            cart.Products.Add(gameId);
            this.ViewData["cart-display"] = "none";

            return new RedirectResponse("/");
        }

        // GET
        public IHttpResponse Remove(int gameId)
        {
            if (!this.gameService.Exist(gameId))
            {
                return new NotFoundResponse();
            }

            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);

            cart.Products.Remove(gameId);

            return new RedirectResponse("/account/cart");
        }

        // POST
        private IHttpResponse Order()
        {
            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);
            var userId = Request.Session.Get<string>(SessionStore.CurrentUserKey);

            this.userService.AddProducts(cart.Products, userId);
            cart.Products.Clear();

            return new RedirectResponse("/");
        }
    }
}
