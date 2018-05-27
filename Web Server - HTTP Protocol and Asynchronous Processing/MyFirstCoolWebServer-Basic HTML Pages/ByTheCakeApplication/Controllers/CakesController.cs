using System.Collections.Generic;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Helpers;
using MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Models;

namespace MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Controllers
{
    public class CakesController : Controller
    {
        private static readonly List<Cake> cakes = new List<Cake>();
        public IHttpResponse Add() => this.FileViewResponse(@"Cakes\add");

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake(name, price);
            cakes.Add(cake);

            return this.FileViewResponse(@"Cakes\add", cake.ToString());
        }

        public IHttpResponse Search() => this.FileViewResponse(@"Cakes\search");
    }
}
