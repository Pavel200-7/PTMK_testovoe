using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTMK_testovoe.Infrastructure.ComponentRegistrar.Registrar;
using System.Reflection;

namespace PTMK_testovoe.Hosts.Console;

public static class Program
{
    public static void Main()
    {
        System.Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();

        services.AddLogging();
        services.RegistrarDbContext(configuration);


        var serviceProvider = services.BuildServiceProvider();

        System.Console.WriteLine("111");

    }
}


