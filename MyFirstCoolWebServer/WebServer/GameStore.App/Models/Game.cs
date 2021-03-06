﻿namespace HTTPServer.GameStore.App.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(11), MaxLength(11)]
        public string TrailerId { get; set; }

        [Required]
        public string ImageTumbnail { get; set; }

        // In GB
        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ICollection<UserGame> Users { get; set; } = new List<UserGame>();
    }
}
