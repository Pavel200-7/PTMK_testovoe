using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMK_testovoe.Application.Services.DbInitService.Repository;

public interface IDbIntitRepository
{
    /// <summary>
    /// Провести миграцию БД.
    /// </summary>
    /// <returns></returns>
    public Task<bool> Migrate();
}
