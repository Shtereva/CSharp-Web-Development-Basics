using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTTPServer.ByTheCakeApplication.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }

        public ICollection<ProductOrder> Orders { get; set; }  = new List<ProductOrder>();
    }
}
