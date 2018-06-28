namespace Kittens.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Username { get; set; }

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
