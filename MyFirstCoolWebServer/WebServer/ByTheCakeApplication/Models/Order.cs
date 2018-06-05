using System;
using System.Collections.Generic;

namespace HTTPServer.ByTheCakeApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ProductOrder> Products { get; set; }  = new List<ProductOrder>();
    }
}
