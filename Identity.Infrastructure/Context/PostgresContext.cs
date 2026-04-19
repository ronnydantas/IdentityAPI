using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Context;

public class PostgresContext : IdentityDbContext<ApplicationUser>
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }
    public DbSet<ApplicationUser> User { get; set; }
    public DbSet<ApplicationRole> Role { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
    }
}
