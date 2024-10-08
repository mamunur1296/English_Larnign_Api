using App.Domain.Abstractions.QueryRepo.Base;
using App.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;


namespace App.Infrastructure.Implementation.Query.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> _dbSet;

        public QueryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllSqlAsync()
        {
            var tableName = typeof(T).Name + "s";
            var sql = $"SELECT * FROM {tableName}";
            return await _dbSet.FromSqlRaw(sql).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdSqlAsync(string id)
        {
            var tableName = typeof(T).Name + "s";
            var sql = $"SELECT * FROM {tableName} WHERE Id = @p0";

            return await _dbSet.FromSqlRaw(sql, id).FirstOrDefaultAsync();
        }
    }
}
