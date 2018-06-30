namespace MeTube.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserBindingModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MinLength(6), MaxLength(50)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
