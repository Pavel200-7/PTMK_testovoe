using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTMK_testovoe.Infrastructure.DataAccess.Contexts;


namespace PTMK_testovoe.Infrastructure.ComponentRegistrar.Registrar;

public static class DbContextRegistrar
{
    public static IServiceCollection RegistrarDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("PTMK_testovoe.Infrastructure.DataAccess")
            );
        });



        return services;
    }
}
