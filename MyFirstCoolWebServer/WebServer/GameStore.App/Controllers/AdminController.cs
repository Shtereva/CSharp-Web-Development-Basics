using System;
using System.Globalization;
using System.Linq;
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
                <a href=""#"" class=""btn btn-warning btn-sm"">Edit</a>
                <a href=""#"" class=""btn btn-danger btn-sm"">Delete</a>
                </td>
                </tr>");

            this.ViewData["games"] = string.Join(Environment.NewLine, gamesResult);

            return this.FileViewResponse(@"/admin/list-games");
        }
    }
}
