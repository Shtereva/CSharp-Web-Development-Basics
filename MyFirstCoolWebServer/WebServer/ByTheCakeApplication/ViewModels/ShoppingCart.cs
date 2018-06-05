using System.Collections.Generic;

namespace HTTPServer.ByTheCakeApplication.ViewModels
{
    public class ShoppingCart
    {
        public const string SessionKey = "%^Current_Shopping_Cart^%";

        public List<int> Products { get; private set; } = new List<int>();
    }
}
