using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PTMK_testovoe.Application.Services.DbInitService.Repository;
using PTMK_testovoe.Infrastructure.DataAccess.Contexts;


namespace PTMK_testovoe.Infrastructure.DataAccess.Repositories;

public class DbIntitRepository : IDbIntitRepository
{
    private ILogger<DbIntitRepository> _logger;
    private readonly AppDbContext _context;

    public DbIntitRepository(ILogger<DbIntitRepository> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<bool> Migrate()
    {
        try
        {
            _logger.LogInformation("Начало наката миграции");

            await _context.Database.MigrateAsync();

            _logger.LogInformation("Миграция успешно применена");

            return true;
        }
        catch (Exception ex) 
        {
            _logger.LogError("Накат миграции провалился");
            _logger.LogError($"Ошибка: {ex.Message}");

            return false;
        }

    }
}
