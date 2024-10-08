using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Abstractions.QueryRepo.Base
{
    public interface IQueryRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllSqlAsync();
        Task<T> GetByIdSqlAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        // Generic repository for all if any
    }
}
