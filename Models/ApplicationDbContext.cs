using temuruang_be.Models;
using Microsoft.EntityFrameworkCore;

namespace temuruang_be;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> User { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<Workspace> Workspace { get; set; }
    public DbSet<Booking> Booking { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(property => property.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(property => property.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.Entity<Article>(entity =>
        {
            entity.Property(property => property.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(property => property.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.Property(property => property.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(property => property.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(property => property.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(property => property.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}