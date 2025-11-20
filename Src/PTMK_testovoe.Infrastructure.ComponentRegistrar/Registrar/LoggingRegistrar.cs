using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PTMK_testovoe.Infrastructure.ComponentRegistrar.Registrar;

public static class LoggingRegistrar
{
    public static IServiceCollection RegistrarLogging(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        return services;
    }

}
