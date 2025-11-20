using Microsoft.Extensions.Logging;
using PTMK_testovoe.Application.Services.DbInitService.Repository;


namespace PTMK_testovoe.Application.Services.DbInitService;

public class DbIntitService : IDbIntitService
{
    private ILogger<DbIntitService> _logger;
    private IDbIntitRepository _repository;

    public DbIntitService(ILogger<DbIntitService> logger, IDbIntitRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<bool> Migrate()
    {
        bool result = await _repository.Migrate();
        return result;
    }
}
