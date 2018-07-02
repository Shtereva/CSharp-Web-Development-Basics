namespace Chushka.App.Controllers
{
    using System.Linq;
    using System.Text;
    using Models.ViewModels;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Interfaces;


    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                var greeting = this.User.IsInRole("1")
                    ? "Enjoy your work today!"
                    : "Feel free to view and order any of our products.";

                var sb = new StringBuilder();

                sb.AppendLine($@"<main class=""mt-3 mb-5"">
                    <div class=""container-fluid text-center"">
                    <h2>Greetings, {this.User.Name}!</h2>
                    <h4>{greeting}</h4>
                    </div>
                    <hr class=""hr-2 bg-dark""/>");

                var allProducts = this.Context.Products
                    .Where(p => !p.IsDeleted)
                    .Select(p => new ProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    })
                    .ToArray();

                if (allProducts.Any())
                {
                    sb.AppendLine(@"<div class=""container-fluid product-holder"">");

                    var startCard = $@"<div class=""row d-flex justify-content-around"">";

                    var endCard = "</div>";

                    sb.AppendLine(startCard);

                    int counter = 1;

                    for (int i = 0; i < allProducts.Length; i++)
                    {
                        var product = allProducts[i];

                        var description = product.Description.Length > 50 
                            ? string.Join("", product.Description.Take(50)) + "..."
                            : product.Description;

                        sb.AppendLine($@"<a href=""/products/details?id={product.Id}"" class=""col-md-2"">
                            <div class=""product p-1 chushka-bg-color rounded-top rounded-bottom"">
                            <h5 class=""text-center mt-3"">{product.Name}</h5>
                            <hr class=""hr-1 bg-white""/>
                            <p class=""text-white text-center"">
                            {description}
                            </p>
                            <hr class=""hr-1 bg-white""/>
                            <h6 class=""text-center text-white mb-3"">${product.Price:f2}</h6>
                            </div>
                            </a>");

                        if (counter == 5)
                        {
                            sb.AppendLine(endCard);
                            sb.AppendLine(startCard);
                            counter = 1;
                            continue;
                        }

                        counter++;
                    }


                    sb.AppendLine(endCard);
                }

                sb.AppendLine($@"</div>");
                sb.AppendLine("</main>");

                this.ViewData["homeView"] = sb.ToString();
            }
            else
            {
                this.ViewData["homeView"] = @"<main>
                    <div class=""jumbotron mt-3 chushka-bg-color"">
                    <h1> Welcome to Chushka Universal Web Shop </h1>
                    <hr class=""bg-white""/>
                    <h3><a class=""nav-link-dark"" href=""/user/login"">Login</a> if you have an account.</h3>
                    <h3><a class=""nav-link-dark"" href=""/user/register"">Register</a> if you don't.</h3>
                    </div>
                    </main>";
            }

            return this.View();
        }
    }
}
