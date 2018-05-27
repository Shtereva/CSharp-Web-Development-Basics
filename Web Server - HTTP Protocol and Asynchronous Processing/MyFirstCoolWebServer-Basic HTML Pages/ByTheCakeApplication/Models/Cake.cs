using System;

namespace MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication.Models
{
    public class Cake
    {
        private string name;

        private decimal price;

        public Cake(string name, string price)
        {
            this.name = name;
            this.price = decimal.Parse(price);
        }

        public override string ToString()
        {
            return $"<div>name: {this.name}</div><div>price: {this.price}</div>";
        }
    }
}
