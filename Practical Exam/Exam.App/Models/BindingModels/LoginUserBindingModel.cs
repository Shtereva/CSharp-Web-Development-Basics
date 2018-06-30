namespace MeTube.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserBindingModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(50)]
        public string Password { get; set; }
    }
}
