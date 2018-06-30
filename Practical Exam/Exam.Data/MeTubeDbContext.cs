namespace MeTube.Data
{
    using MeTube.Models;
    using Microsoft.EntityFrameworkCore;

    public class MeTubeDbContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=MeTube;Integrated Security=True";

        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.Property(u => u.Username).IsRequired().HasMaxLength(20);
                e.Property(u => u.PasswordHash).IsRequired();
                e.Property(u => u.Email).IsRequired();

                e.HasMany(u => u.Tubes)
                    .WithOne(t => t.Uploader)
                    .HasForeignKey(t => t.UploaderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Tube>(e =>
            {
                e.Property(t => t.Title).IsRequired().HasMaxLength(50);
                e.Property(t => t.Author).IsRequired().HasMaxLength(20);
                e.Property(t => t.Description).IsRequired().HasMaxLength(250);
                e.Property(t => t.YoutubeId).IsRequired();
            });
        }
    }
}
