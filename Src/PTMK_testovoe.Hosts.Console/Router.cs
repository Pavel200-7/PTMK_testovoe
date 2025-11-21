using MediatR;
using Microsoft.Extensions.Logging;
using PTMK_testovoe.Application.Services.DbInitService;
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
}
