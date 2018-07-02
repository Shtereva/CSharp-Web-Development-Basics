namespace Chushka.Models
{
    using System.Collections.Generic;

    public class FoodType
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }  = new List<Product>();
    }
}
