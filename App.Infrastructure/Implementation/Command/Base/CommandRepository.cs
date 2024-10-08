using App.Domain.Abstractions.CommandRepo.Base;
using App.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Implementation.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> _dbSet;

        public CommandRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }

        public async Task AddSqlAsync(T entity)
        {
            var (sql, parameters) = GenerateInsertSqlWithParams(entity);
            await ExecuteNonQueryAsync(sql, parameters);
        }

        public async Task UpdateSqlAsync(T entity)
        {
            var (sql, parameters) = GenerateUpdateSqlWithParams(entity);
            await ExecuteNonQueryAsync(sql, parameters);
        }

        public async Task DeleteSqlAsync(string id)
        {
            var tableName = typeof(T).Name + "s";
            var sql = $"DELETE FROM {tableName} WHERE Id = @p0";
            await ExecuteNonQueryAsync(sql, id);
        }

        // Execute SQL for fetching data
        public async Task<IEnumerable<T>> ExecuteRawSqlAsync(string sql, params object[] parameters)
        {
            return await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
        }

        // Execute SQL for non-query operations (INSERT, UPDATE, DELETE)
        public async Task ExecuteNonQueryAsync(string sql, params object[] parameters)
        {
            await _applicationDbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        // Helper method to generate parameterized INSERT SQL
        private (string, object[]) GenerateInsertSqlWithParams(T entity)
        {
            var properties = entity.GetType().GetProperties();

            // Assuming table name is plural of the entity type
            var tableName = typeof(T).Name + "s";

            // Get property names for SQL column names
            var columnNames = string.Join(",", properties.Select(p => p.Name));

            // Generate parameter names (@p0, @p1, ...)
            var paramNames = string.Join(",", properties.Select((p, index) => $"@p{index}"));

            // Collect parameter values, allowing nulls where appropriate
            var values = properties.Select(p =>
            {
                var value = p.GetValue(entity);
                // Use null for nullable properties and non-nullable ones where appropriate
                return value ?? null;
            }).ToArray();

            // SQL insert statement
            var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({paramNames})";

            // Return SQL query and corresponding parameter values
            return (sql, values);
        }




        // Helper method to generate parameterized UPDATE SQL
        private (string, object[]) GenerateUpdateSqlWithParams(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var tableName = typeof(T).Name + "s"; // Assuming table name is plural

            // Identify the 'Id' property
            var idProperty = properties.FirstOrDefault(p => p.Name == "Id");
            if (idProperty == null)
            {
                throw new ArgumentException("Entity must have an 'Id' property");
            }

            // Get the 'Id' value for the WHERE clause
            var idValue = idProperty.GetValue(entity) ?? DBNull.Value;

            // Create the update pairs for non-'Id' properties
            var updatePairs = string.Join(",", properties.Where(p => p.Name != "Id")
                                                         .Select((p, index) => $"{p.Name}=@p{index}"));

            // Get values for non-'Id' properties, using DBNull.Value for nulls
            var values = properties.Where(p => p.Name != "Id")
                                   .Select(p => p.GetValue(entity) ?? DBNull.Value).ToArray();

            // Create the SQL statement, using the 'Id' in the WHERE clause
            var sql = $"UPDATE {tableName} SET {updatePairs} WHERE Id=@p{properties.Length - 1}";

            // Add the 'Id' value to the parameter list
            var parameters = values.Concat(new[] { idValue }).ToArray();

            return (sql, parameters);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
