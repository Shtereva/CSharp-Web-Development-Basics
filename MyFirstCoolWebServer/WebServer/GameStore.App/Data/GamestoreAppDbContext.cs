namespace HTTPServer.GameStore.App.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class GamestoreAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        private const string ConnectionString = "Server=.;Database=GameStore;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGame>()
                .HasKey(ug => new
                {
                    ug.UserId,
                    ug.GameId
                });

            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(u => u.Email).IsUnique();
                e.Property(u => u.Email).IsRequired();
                e.Property(u => u.Password).IsRequired();
                e.Property(u => u.FullName).IsRequired();
            });

            modelBuilder.Entity<Game>(e =>
            {
                e.Property(g => g.Title).IsRequired().HasMaxLength(100);
                e.Property(g => g.TrailerId).IsRequired().IsFixedLength();
                e.Property(g => g.ImageTumbnail).IsRequired();
                e.Property(g => g.Description).IsRequired();
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Games)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Users)
                .WithOne(u => u.Game)
                .HasForeignKey(u => u.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
