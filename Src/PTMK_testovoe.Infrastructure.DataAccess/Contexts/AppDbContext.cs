using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PTMK_testovoe.Domain.Entities;


namespace PTMK_testovoe.Infrastructure.DataAccess.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employee { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(
                "Host=localhost;Port=5432;Database=PTMK_testovoe;Username=postgres;Password=iamdeadlytired795795",
                b => b.MigrationsAssembly("PTMK_testovoe.Infrastructure.DataAccess")
            );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
