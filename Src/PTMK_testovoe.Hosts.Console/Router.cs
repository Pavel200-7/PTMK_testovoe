using MediatR;
using Microsoft.Extensions.Logging;
using PTMK_testovoe.Application.Services.DbInit;
using PTMK_testovoe.Application.Services.Employee.Commands.CreateEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMK_testovoe.Hosts.Console;

public class Router
{
    private ILogger<Router> _logger;
    private IMediator _mediator;

    public Router(ILogger<Router> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Migrate()
        => await _mediator.Send(new MigrateDbCommand());

    public async Task CreateEmployee(string fullName, string birthDate, string gender)
    {
        _logger.LogInformation("Начало создания пользователя");


        await _mediator.Send(new CreateEmployeeCommand()
        {
            FullName = fullName,
            BirthDate = birthDate,
            Gender = gender
        });

    }
}
