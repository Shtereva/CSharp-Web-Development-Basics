using System;
using System.Globalization;
using HTTPServer.GameStore.App.Services;
using HTTPServer.GameStore.App.Services.Contracts;
using HTTPServer.GameStore.App.ViewModels.Admin;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.GameStore.App.Controllers
{
    using Server.Http.Contracts;

    public class AdminController : BaseController
    {
        private const string AddGamePath = @"/admin/add-game";

        private readonly IGameService gameService;
        public AdminController(IHttpRequest req) : base(req)
        {
            this.gameService = new GameService();
        }

        public IHttpResponse AddGame()
        {
            if (!this.Authentication.IsAdmin)
            {
                return new RedirectResponse(@"/");
            }

            return this.FileViewResponse(AddGamePath);
        }

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

            return new RedirectResponse(@"/admin/games/list");
        }

        public IHttpResponse List()
        {
            // TODO: Proceed from here
            return null;
        }
    }
}
