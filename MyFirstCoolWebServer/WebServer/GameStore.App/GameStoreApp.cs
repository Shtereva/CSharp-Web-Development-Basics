namespace HTTPServer.GameStore.App
{
    using ViewModels.Account;
    using ViewModels.Admin;
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
            appRouteConfig.AnonymousPaths.Add(@"/account/login");
            appRouteConfig.AnonymousPaths.Add(@"/account/register");
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

            appRouteConfig
                .Get(
                    @"/admin/games/edit/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Edit(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(
                    @"/admin/games/edit/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Edit(new AddGameViewModel()
                    {
                        Id = req.UrlParameters["id"],
                        Title = req.FormData["title"],
                        Description = req.FormData["description"],
                        ImageTumbnail = req.FormData["thumbnail"],
                        Price = req.FormData["price"],
                        Size = req.FormData["size"],
                        TrailerId = req.FormData["url"],
                        ReleaseDate = req.FormData["date"]
                    }));

            appRouteConfig
                .Get(
                    @"/admin/games/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(
                    @"/admin/games/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete(new DeleteGameViewModel()
                    {
                        GameId = req.UrlParameters["id"]
                    }));

            appRouteConfig
                .Get(
                    "/admin/games/info/{(?<id>[0-9]+)}",
                    req => new HomeController(req).Info(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(
                    "/account/cart",
                    req => new CartController(req).ShowCart());

            appRouteConfig
                .Post(
                    "/account/cart/",
                    req => new CartController(req).ShowCart());

            appRouteConfig
                .Get(
                    "/home/buy/{(?<id>[0-9]+)}",
                    req => new CartController(req).Add(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(
                    "/home/remove/{(?<id>[0-9]+)}",
                    req => new CartController(req).Remove(int.Parse(req.UrlParameters["id"])));
        }
    }
}
