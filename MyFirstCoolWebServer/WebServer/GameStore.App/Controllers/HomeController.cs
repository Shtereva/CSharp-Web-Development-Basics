namespace HTTPServer.GameStore.App.Controllers
{
    using Server.Http;
    using System.Text;
    using ViewModels;
    using System.Linq;
    using Services;
    using Services.Contracts;

    using Server.Http.Contracts;
    public class HomeController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IUserService userService;

        public HomeController(IHttpRequest req) : base(req)
        {
            this.gameService = new GameService();
            this.userService = new UserService();
        }
        public IHttpResponse Index()
        {
            var user = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
            var sb = new StringBuilder();
            var filter = "All";

            if (this.Request.UrlParameters.ContainsKey("filter")
                && this.Request.UrlParameters["filter"] == "Owned"
                && this.Authentication.IsAuthenticated)
            {
                filter = "Owned";
            }

            var listGames = this.gameService.List(user, filter).ToArray();

            string endCard = this.CreateHtml(user, sb, listGames);

            sb.AppendLine(endCard);

            this.ViewData["home-games"] = sb.ToString();

            return this.FileViewResponse(@"/home/index");
        }

        private string CreateHtml(string user, StringBuilder sb, ViewModels.Home.AllGamesViewModel[] listGames)
        {
            var startCard = $@"<div class=""card-group"">";
            var endCard = "</div>";
            var adminHomeDisplay = this.Authentication.IsAdmin ? "inline-block" : "none";

            sb.AppendLine(startCard);
            int counter = 1;

            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);

            for (int i = 0; i < listGames.Length; i++)
            {
                var cartDisplay = "inline-block";

                var game = listGames[i];

                if ((user != null && this.userService.GameExist(int.Parse(game.Id), user))
                    || (cart != null && cart.Products.Contains(int.Parse(game.Id))))
                {
                    cartDisplay = "none";
                }

                var descriptionAsList = game.Description.ToCharArray().Take(300).ToList();
                var indexOfLastDot = descriptionAsList.LastIndexOf('.');
                if (indexOfLastDot == -1)
                {
                    indexOfLastDot = 300;
                }

                var description = string.Join("", descriptionAsList.Take(indexOfLastDot + 1));

                var result = $@"<div class=""card col-4 thumbnail""> 
                <img class=""card-image-top img-fluid img-thumbnail"" onerror=""this.src='{game.ImageTumbnail}';"" src=""{game.ImageTumbnail}"">
                <div class=""card-body"">
                <h4 class=""card-title"">{game.Title}</h4>
                <p class=""card-text""><strong>Price</strong> - {game.Price:f2}&euro;</p>
                <p class=""card-text""><strong>Size</strong> - {game.Size:f1} GB</p>
                <p class=""card-text"">{description}</p>
                </div>
                <div class=""card-footer"">
                <a class=""card-button btn btn-warning"" style=""display: {adminHomeDisplay}"" name=""edit"" 
                  href=""/admin/games/edit/{game.Id}"">Edit</a>
                <a class=""card-button btn btn-danger"" style=""display: {adminHomeDisplay}"" name=""delete"" href=""/admin/games/delete/{game.Id}"">Delete</a>
                <a class=""card-button btn btn-outline-primary"" name=""info"" href=""/admin/games/info/{game.Id}"">Info</a>
                <a class=""card-button btn btn-primary"" style=""display: {cartDisplay}
                ""name=""buy"" href=""/home/buy/{game.Id}"">Buy</a>
                </div>
                </div>";

                sb.AppendLine(result);

                if (counter == 3)
                {
                    sb.AppendLine(endCard);
                    sb.AppendLine(startCard);
                    counter = 1;
                    continue;
                }

                counter++;
            }

            return endCard;
        }

        public IHttpResponse Info(int id)
        {
            var game = this.gameService.Find(id);

            this.ViewData["title"] = game.Title;
            this.ViewData["video-id"] = game.TrailerId;
            this.ViewData["description"] = game.Description;
            this.ViewData["price"] = game.Price;
            this.ViewData["size"] = game.Size;
            this.ViewData["date"] = game.ReleaseDate;
            this.ViewData["game-id"] = id.ToString();

            var cartDisplay = "inline-block";
            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);
            var user = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

            if ((user != null && this.userService.GameExist(id, user))
                || (cart != null && cart.Products.Contains(id)))
            {
                cartDisplay = "none";
            }

            this.ViewData["cart-display"] = cartDisplay;

            return this.FileViewResponse(@"/home/game-details");
        }
    }
}
