namespace HTTPServer.GameStore.App.ViewModels
{
    using System.Collections.Generic;
    public class Cart
    {
        public const string SessionKey = "%^Current_Shopping_Cart^%";

        public List<int> Products { get; private set; } = new List<int>();
    }
}
