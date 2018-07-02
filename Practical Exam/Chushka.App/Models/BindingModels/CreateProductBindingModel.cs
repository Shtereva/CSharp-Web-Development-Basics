namespace Chushka.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateProductBindingModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int FoodTypeId { get; set; }
    }
}
