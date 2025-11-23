using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PTMK_testovoe.Application.Services.DbInit;
using PTMK_testovoe.Application.Services.Employee.Commands.CreateEmploreeMass;
using PTMK_testovoe.Application.Services.Employee.Commands.CreateEmployee;
using PTMK_testovoe.Application.Services.Employee.Queries.GetEmployee;
using PTMK_testovoe.Application.Services.Employee.Queries.GetEmployeeMaleWithStartingLastnameF;
using System.Diagnostics;
using System.Reflection;
using System.Threading;


namespace PTMK_testovoe.Hosts.Console;

public class Router
{
    private ILogger<Router> _logger;
    private IMediator _mediator;
    private Stopwatch _stopwatch;
    private IRequestHandler<GetEmployeeMaleWithStartingLastnameFQuery, List<GetEmployeeMaleWithStartingLastnameFResponce>> _request5Handler;


    public Router(
        ILogger<Router> logger, 
        IMediator mediator, 
        Stopwatch stopwatch, 
        IRequestHandler<GetEmployeeMaleWithStartingLastnameFQuery, List<GetEmployeeMaleWithStartingLastnameFResponce>> request5Handler
        )
    {
        _logger = logger;
        _mediator = mediator;
        _stopwatch = stopwatch;
        _request5Handler = request5Handler;
    }

    public async Task Migrate()
    {
        try
        {
            bool success = await _mediator.Send(new MigrateDbCommand());
            if (success)
            {
                System.Console.WriteLine("Схема базы данных готова");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Возникли ошибки");
            System.Console.WriteLine(ex.Message);
        }
    }

    public async Task CreateEmployee(string fullName, string birthDate, string gender)
    {
        try
        {
            bool success = await _mediator.Send(new CreateEmployeeCommand()
            {
                FullName = fullName,
                BirthDate = birthDate,
                Gender = gender
            });

            if (success)
            {
                System.Console.WriteLine("Пользователь успешно создан");
            }
        }
        catch (ValidationException ex)
        {
            System.Console.WriteLine("Возникли ошибки валидации");
            foreach (var item in ex.Errors)
            {
                System.Console.WriteLine($"{item.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    public async Task GetEmployee()
    {
        try
        {
            List<GetEmployeeResponce> employee = await _mediator.Send(new GetEmployeeQuery());
            foreach (var responce in employee)
            {
                System.Console.WriteLine($"ФИО: {responce.FullName}, Дата рождения: {responce.BirthDate}, Пол: {responce.Gender}, Количество полных лет: {responce.FullYears}");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    public async Task Create100000Employee()
    {
        try
        {
            bool success = await _mediator.Send(new CreateEmployeeMassCommand());
            if (success)
            {
                System.Console.WriteLine("Добавление 100000 сотрудников прошло успешно");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    public async Task GetEmployeeMaleWithStartingLastnameF()
    {
        try
        {
            _stopwatch.Start();
            CancellationToken cancellationToken = new CancellationToken();
            List<GetEmployeeMaleWithStartingLastnameFResponce> employee = await _request5Handler.Handle(new GetEmployeeMaleWithStartingLastnameFQuery(), cancellationToken);

            _stopwatch.Stop();

            //foreach (var responce in employee)
            //{
            //    System.Console.WriteLine($"ФИО: {responce.FullName}, Дата рождения: {responce.BirthDate}, Пол: {responce.Gender}");
            //}

            _logger.LogInformation("Итоговое время выполнения" + _stopwatch.ElapsedMilliseconds + " ms");

            //System.Console.WriteLine("Время выполнения (без учета вывода)" + stopWatch.ElapsedMilliseconds + " ms");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

}