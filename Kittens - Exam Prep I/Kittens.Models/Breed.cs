namespace Kittens.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Breed
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<Kitten> Kittens { get; set; } = new List<Kitten>();
    }
}
