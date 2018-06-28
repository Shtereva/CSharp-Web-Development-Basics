namespace Kittens.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddKittenModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(0, 15)]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }
    }
}
