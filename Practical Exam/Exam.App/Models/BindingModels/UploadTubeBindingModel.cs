namespace MeTube.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class UploadTubeBindingModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Author { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
