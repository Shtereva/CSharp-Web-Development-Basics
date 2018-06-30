namespace MeTube.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tube
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Author { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        public int Views { get; set; }

        public int UploaderId { get; set; }
        public User Uploader { get; set; }
    }
}
