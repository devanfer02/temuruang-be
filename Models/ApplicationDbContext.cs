using temuruang_be.Models;
using Microsoft.EntityFrameworkCore;

namespace temuruang_be;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> User { get; set; }
}