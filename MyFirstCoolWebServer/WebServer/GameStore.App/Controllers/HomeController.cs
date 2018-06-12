using System.Text;

namespace HTTPServer.GameStore.App.Controllers
{
    using System;
    using System.Linq;
    using Services;
    using Services.Contracts;

    using Server.Http.Contracts;
    public class HomeController : BaseController
    {
        private readonly IGameService gameService;

        public HomeController(IHttpRequest req) : base(req)
        {
            this.gameService = new GameService();
        }
        public IHttpResponse Index()
        {
            var listGames = this.gameService
                .List()
                .ToArray();

            var startCard = $@"<div class=""card-group"">";
            var endCard = "</div>";
            var adminHomeDisplay = this.Authentication.IsAdmin ? "inline-block" : "none";

            var sb = new StringBuilder();
            sb.AppendLine(startCard);
            int counter = 1;

            for (int i = 0; i < listGames.Length; i++)
            {
                var game = listGames[i];

                var result = $@"<div class=""card col-4 thumbnail""> 
                <img class=""card-image-top img-fluid img-thumbnail"" onerror=""this.src='{game.ImageTumbnail}';"" src=""{game.ImageTumbnail}"">
                <div class=""card-body"">
                <h4 class=""card-title"">{game.Title}</h4>
                <p class=""card-text""><strong>Price</strong> - {game.Price:f2}&euro;</p>
                <p class=""card-text""><strong>Size</strong> - {game.Size:f1} GB</p>
                <p class=""card-text"">{game.Description}</p>
                </div>
                <div class=""card-footer"">
                <a class=""card-button btn btn-warning"" style=""display: {adminHomeDisplay}"" name=""edit"" 
                  href=""#"">Edit</a>
                <a class=""card-button btn btn-danger"" style=""display: {adminHomeDisplay}"" name=""delete"" href=""#"">Delete</a>
                <a class=""card-button btn btn-outline-primary"" name=""info"" href=""#"">Info</a>
                <a class=""card-button btn btn-primary"" name=""buy"" href=""#"">Buy</a>
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

            sb.AppendLine(endCard);

            this.ViewData["home-games"] = sb.ToString();

            return this.FileViewResponse(@"/home/index");
        }
    }
}
