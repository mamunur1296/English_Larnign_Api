namespace App.Domain.Abstractions.CommandRepo.Base
{
    public interface ICommandRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task AddSqlAsync(T entity);
        Task UpdateSqlAsync(T entity);
        Task DeleteSqlAsync(string id);
        Task<IEnumerable<T>> ExecuteRawSqlAsync(string sql, params object[] parameters);
        Task ExecuteNonQueryAsync(string sql, params object[] parameters);
    }
}
