using MediatR;
using Microsoft.Extensions.Logging;
using PTMK_testovoe.Application.Services.DbInitService.Repository;

namespace PTMK_testovoe.Application.Services.DbInitService;

public class DbIntitHandler : IRequestHandler<MigrateDbCommand, bool>
{
    private ILogger<DbIntitHandler> _logger;
    private IDbIntitRepository _repository;

    public DbIntitHandler(ILogger<DbIntitHandler> logger, IDbIntitRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<bool> Handle(MigrateDbCommand request, CancellationToken cancellationToken)
    {
        bool result = await _repository.Migrate();
        return result;
    }
}
