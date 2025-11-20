using Microsoft.Extensions.DependencyInjection;
using PTMK_testovoe.Application.Services.DbInitService.Repository;
using PTMK_testovoe.Infrastructure.DataAccess.Repositories;


namespace PTMK_testovoe.Infrastructure.ComponentRegistrar.Registrar;

public static class ComponentRegistgar
{
    public static IServiceCollection RegistrarComponents(this IServiceCollection services)
    {
        //services.AddScoped<>


        return services;
    }


    public static IServiceCollection RegistrarInfrastructureComponents(this IServiceCollection services)
    {
        services.AddScoped<IDbIntitRepository, DbIntitRepository>();

        return services;
    }


}
