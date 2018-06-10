using HTTPServer.ByTheCakeApplication.ViewModels;
using HTTPServer.ByTheCakeApplication.ViewModels.Account;
using Microsoft.EntityFrameworkCore;

namespace HTTPServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using Data;

    public class ByTheCakeApp : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new ByTheCakeDbContext())
            {
                db.Database.Migrate();
            }
        }
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add("/login");
            appRouteConfig.AnonymousPaths.Add("/register");

            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());

            appRouteConfig
                .Get("/add", req => new ProductController().Add());

            appRouteConfig
                .Post(
                    "/add",
                    req => new ProductController().Add(new ProductViewModel()
                    {
                        Name = req.FormData["name"],
                        Price = decimal.Parse(req.FormData["price"]),
                        ImageUrl = req.FormData["image-url"]
                    }));

            appRouteConfig
                .Get(
                    "/search", 
                    req => new ProductController().Search(req));

            appRouteConfig
                .Get(
                    "/products/{(?<id>[0-9]+)}",
                    req => new ProductController().Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(
                    "/login",
                    req => new AccountController().Login());

            appRouteConfig
                .Post(
                    "/login",
                    req => new AccountController().Login(req, new LoginUserViewModel()
                    {
                        Username = req.FormData["name"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Post(
                    "/logout",
                    req => new AccountController().Logout(req));

            appRouteConfig
                .Get(
                    "/register",
                    req => new AccountController().Register());

            appRouteConfig
                .Post(
                    "/register",
                    req => new AccountController().Register(req, new RegisterUserViewModel()
                    {
                        Username = req.FormData["name"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]
                    }));

            appRouteConfig
                .Get(
                    "/shopping/add/{(?<id>[0-9]+)}",
                    req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get(
                    "/cart",
                    req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post(
                    "/shopping/finish-order",
                    req => new ShoppingController().FinishOrder(req));

            appRouteConfig
                .Get(
                    "/profile",
                    req => new AccountController().ViewProfile(req));

            appRouteConfig
                .Get(
                    "/orders",
                    req => new ShoppingController().ListOrders(req));

            appRouteConfig
                .Get(
                    "/orderdetails/{(?<id>[0-9]+)}",
                    req => new ShoppingController().OrderDetails(int.Parse(req.UrlParameters["id"])));
        }
    }
}
