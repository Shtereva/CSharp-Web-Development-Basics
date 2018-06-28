namespace Kittens.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Kitten
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(0, 15)]
        public int Age { get; set; }

        [Required]
        public int BreedId { get; set; }
        public Breed Breed { get; set; }
    }
}
