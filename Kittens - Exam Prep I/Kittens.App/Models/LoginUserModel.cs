namespace Kittens.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
