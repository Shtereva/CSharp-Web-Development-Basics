namespace HTTPServer.GameStore.App.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [MinLength(6), MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
