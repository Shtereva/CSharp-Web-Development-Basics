namespace HTTPServer.GameStore.App.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Services;
    using Services.Contracts;
    using ViewModels.Admin;
    using Server.Http.Response;
    using Server.Http.Contracts;

    public class AdminController : BaseController
    {
        private const string AddGamePath = @"/admin/add-game";
        private const string EditGamePath = @"/admin/edit-game";
        private const string DeleteGamePath = @"/admin/delete-game";
        private const string ListGamesPath = @"/admin/list-games";

        private readonly IGameService gameService;
        public AdminController(IHttpRequest req) : base(req)
        {
            this.gameService = new GameService();
        }

        // GET
        public IHttpResponse AddGame()
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse(@"/");
            }

            return this.FileViewResponse(AddGamePath);
        }

        //POST
        public IHttpResponse AddGame(AddGameViewModel viewModel)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse(@"/");
            }

            if (!Validator.Title(viewModel.Title) && !Validator.Price(viewModel.Price) && !Validator.Size(viewModel.Size)
                && !Validator.Trailer(viewModel.TrailerId) && !Validator.Description(viewModel.Description))
            {
                this.ViewData["showError"] = "block";
                this.ViewData["errorMessage"] = "Please fill out all fields correctly.";

                return this.FileViewResponse(AddGamePath);
            }

            var date = DateTime.ParseExact(viewModel.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var success = this.gameService.Create(
                            viewModel.Title,
                            viewModel.Description,
                            viewModel.ImageTumbnail,
                            decimal.Parse(viewModel.Price),
                            double.Parse(viewModel.Size),
                            viewModel.TrailerId,
                            date);

            if (!success)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["errorMessage"] = "Please fill out all fields correctly.";

                return this.FileViewResponse(AddGamePath);
            }

            return new RedirectResponse(@"/");
        }

        // GET
        public IHttpResponse List()
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse(@"/");
            }

            var gamesResult = this.gameService
                                .All()
                                .Select(g => $@"<tr class=""table-warning"">
                <th scope=""row"">{g.Id}</th>
                <td>{g.Name}</td>
                <td>{g.Size:f1} GB</td>
                <td>{g.Price:f2} &euro;</td>
                <td>
                <a href=""/admin/games/edit/{g.Id}"" class=""btn btn-warning btn-sm"">Edit</a>
                <a href=""/admin/games/delete/{g.Id}"" class=""btn btn-danger btn-sm"">Delete</a>
                </td>
                </tr>");

            this.ViewData["games"] = string.Join(Environment.NewLine, gamesResult);

            return this.FileViewResponse(ListGamesPath);
        }

        // GET
        public IHttpResponse Edit(int id)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse(@"/");
            }

            var game = this.gameService.Find(id);

            if (game == null)
            {
                return new RedirectResponse(@"/");
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["thumbnail"] = game.ImageTumbnail;
            this.ViewData["price"] = game.Price;
            this.ViewData["size"] = game.Size;
            this.ViewData["url"] = game.TrailerId;
            this.ViewData["date"] = game.ReleaseDate;

            return this.FileViewResponse(EditGamePath);
        }

        // POST
        public IHttpResponse Edit(AddGameViewModel viewModel)
        {
            this.gameService.Edit(int.Parse(viewModel.Id), viewModel);

            return new RedirectResponse(@"/");
        }

        // GET
        public IHttpResponse Delete(int id)
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse(@"/");
            }

            var game = this.gameService.Find(id);

            if (game == null)
            {
                return new RedirectResponse(@"/");
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["thumbnail"] = game.ImageTumbnail;
            this.ViewData["price"] = game.Price;
            this.ViewData["size"] = game.Size;
            this.ViewData["url"] = game.TrailerId;
            this.ViewData["date"] = game.ReleaseDate;

            return this.FileViewResponse(DeleteGamePath);
        }

        // POST
        public IHttpResponse Delete(DeleteGameViewModel viewModel)
        {

            this.gameService.Delete(int.Parse(viewModel.GameId));

            return new RedirectResponse(@"/");
        }
    }
}
