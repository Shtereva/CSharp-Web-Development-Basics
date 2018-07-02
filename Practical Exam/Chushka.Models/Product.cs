namespace Chushka.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public int FoodTypeId  { get; set; }
        public FoodType FoodType { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
