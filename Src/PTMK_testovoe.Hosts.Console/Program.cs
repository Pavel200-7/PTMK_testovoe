using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTMK_testovoe.Infrastructure.ComponentRegistrar.Registrar;
using System.CommandLine;
using System.IO;
using System.Reflection;


namespace PTMK_testovoe.Hosts.Console;

public static class Program
{
    public static int Main(string[] args)
    {
        System.Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();

        // Регистрация зависимостей в DI
        services.RegistrarLogging();
        services.RegistrarDbContext(configuration);
        services.RegistrarInfrastructureComponents();
        services.RegistrarComponents();
        services.RegistrarAutomapper();
        services.RegistrarMediatR();
        
        services.AddScoped<Router>();

        var serviceProvider = services.BuildServiceProvider();

        //  Получение объекта - роутера
        using var scope = serviceProvider.CreateScope();
        var router = scope.ServiceProvider.GetRequiredService<Router>();

        // Настройка входных аргументов и их обработчиков
        RootCommand rootCommand = new();

        // Команда миграции (режим 1)
        Command CreateTableCommand = new("1");
        rootCommand.Add(CreateTableCommand);
        CreateTableCommand.SetAction(ParseResult => router.Migrate());


        Command CreateEmployeeCommand = new("2");
        Argument<string> fullName = new("fullName") { Arity = ArgumentArity.ExactlyOne };
        Argument<string> birthDate = new("birthDate") { Arity = ArgumentArity.ExactlyOne };
        Argument<string> gender = new("gender") { Arity = ArgumentArity.ExactlyOne };
        CreateEmployeeCommand.Arguments.Add(fullName) ;
        CreateEmployeeCommand.Arguments.Add(birthDate);
        CreateEmployeeCommand.Arguments.Add(gender);
        rootCommand.Add(CreateEmployeeCommand);
        CreateEmployeeCommand.SetAction(ParseResult => router.CreateEmployee(
            ParseResult.GetValue(fullName),
            ParseResult.GetValue(birthDate),
            ParseResult.GetValue(gender)
            ));

        Command GetEmployeeCommand = new("3");
        rootCommand.Add(GetEmployeeCommand);
        GetEmployeeCommand.SetAction(ParseResult => router.GetEmployee());


        return rootCommand.Parse(args).Invoke();
    }

    
}


