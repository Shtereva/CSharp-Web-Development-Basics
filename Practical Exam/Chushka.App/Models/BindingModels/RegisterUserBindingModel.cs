namespace Chushka.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserBindingModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Username { get; set; }

        public string FullName { get; set; }

        [Required]
        [MinLength(6), MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [MinLength(6), MaxLength(20)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
