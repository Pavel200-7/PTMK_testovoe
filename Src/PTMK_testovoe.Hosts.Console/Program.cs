using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTMK_testovoe.Infrastructure.ComponentRegistrar.Registrar;
using System.CommandLine;
using System.Reflection;
using MediatR;


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
        services.RegistrarInfrastructureComponents();
        services.RegistrarComponents();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


        var serviceProvider = services.BuildServiceProvider();



        RootCommand rootCommand = new();

        Command CreateTableCommand = new("1");
        rootCommand.Add(CreateTableCommand);

        //CreateTableCommand.SetAction(ParseResult =>)





        System.Console.WriteLine("111");

    }

    
}


