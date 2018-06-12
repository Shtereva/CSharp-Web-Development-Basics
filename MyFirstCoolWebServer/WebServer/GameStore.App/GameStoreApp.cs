using System;
using System.Globalization;
using HTTPServer.GameStore.App.ViewModels.Account;
using HTTPServer.GameStore.App.ViewModels.Admin;

namespace HTTPServer.GameStore.App
{
    using Controllers;
    using Data;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using Microsoft.EntityFrameworkCore;
    public class GameStoreApp  : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new GamestoreAppDbContext())
            {
                db.Database.Migrate();
            }
        }
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add(@"/account/register");
            appRouteConfig.AnonymousPaths.Add(@"/account/login");
            appRouteConfig.AnonymousPaths.Add(@"/");

            appRouteConfig
                .Get(
                    "/",
                    req => new HomeController(req).Index());

            appRouteConfig
                .Get(
                    "/account/register",
                    req => new AccountController(req).Register());

            appRouteConfig
                .Post(
                    "/account/register",
                    req => new AccountController(req).Register(new RegisterViewModel()
                    {
                        Email = req.FormData["email"],
                        FullName = req.FormData["full-name"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]
                    }));

            appRouteConfig
                .Get(
                    "/account/login",
                    req => new AccountController(req).LogIn());

            appRouteConfig
                .Post(
                    "/account/login",
                    req => new AccountController(req).LogIn(new LoginViewModel()
                    {
                        Email = req.FormData["email"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get(
                    "/account/logout",
                    req => new AccountController(req).LogOut());

            appRouteConfig
                .Get(
                    @"/admin/games/add",
                    req => new AdminController(req).AddGame());

            appRouteConfig
                .Post(
                    @"/admin/games/add",
                    req => new AdminController(req).AddGame(new AddGameViewModel()
                    {
                        Title = req.FormData["title"],
                        Description = req.FormData["description"],
                        ImageTumbnail = req.FormData["thumbnail"],
                        Price = req.FormData["price"],
                        Size = req.FormData["size"],
                        TrailerId = req.FormData["video-id"],
                        ReleaseDate = req.FormData["release-date"]
                    }));

            appRouteConfig
                .Get(
                    @"/admin/games/list",
                    req => new AdminController(req).List());
        }
    }
}
