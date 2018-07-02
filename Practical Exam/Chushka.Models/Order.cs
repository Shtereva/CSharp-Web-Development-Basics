namespace Chushka.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ClientId { get; set; }
        public User Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
