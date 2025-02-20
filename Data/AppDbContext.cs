using Microsoft.EntityFrameworkCore;
using VideoshopAPIV3.Model;

namespace VideoshopAPIV3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        public DbSet<RentalHeader> RentalHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalHeader>()
                        .HasOne(c => c.Customers)
                        .WithMany(r => r.RentalHeaders)
                        .HasForeignKey(k => k.CustomerId);

            modelBuilder.Entity<RentalDetail>()
                        .HasOne(r => r.RentalHeader)
                        .WithMany(r => r.RentalDetails)
                        .HasForeignKey(k => k.RentalHeaderId);

            modelBuilder.Entity<RentalDetail>()
                        .HasOne(m => m.Movie)
                        .WithMany(m => m.RentalDetails)
                        .HasForeignKey(k => k.MovieId);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasPrecision(10,2);
            });

            modelBuilder.Entity<RentalDetail>(entity =>
            {
                entity.HasKey(i => i.Id);
                
            });
        }
    }
}
