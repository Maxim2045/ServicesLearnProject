using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;

namespace BaseRepositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        public readonly IOptions<DbOptions> _dbOptions;
        public readonly string _connectionString;
        public readonly string _table;
        //  private BaseEntity _currentContextAccessor;

        public BaseRepository(IOptions<DbOptions> dbOptions, string table)
        {
            _dbOptions = dbOptions;
            _connectionString = _dbOptions.Value.ConnectionString;
            _table = table;
        }
        public virtual async Task<T> GetById(Guid id)
        {
            await using var db = await GetSqlConnection();
            return await db.QueryFirstOrDefaultAsync<T>(
                $"SELECT * FROM {_table} WHERE Id = @Id AND IsDeleted = 0", new { Id = id });
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            await using var db = await GetSqlConnection();
            return await db.QueryAsync<T>($"SELECT * FROM {_table} WHERE IsDeleted = 0");
        }

        public virtual async Task Create(T entity)
        {
            try
            {
                await using var db = await GetSqlConnection();

                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreatedDate = DateTime.UtcNow;
                entity.LastSavedDate = DateTime.UtcNow;
                var fields = string.Join(", ", typeof(T).GetProperties().Select(prop => $"[{prop.Name}]"));
                var values = string.Join(", ", typeof(T).GetProperties().Select(prop => $"@{prop.Name}"));
                await db.ExecuteAsync($"INSERT INTO {_table} ({fields}) VALUES ({values})", entity);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual async Task CreateMany(IEnumerable<T> entities)
        {
            await using var db = await GetSqlConnection();

            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreatedDate = DateTime.UtcNow;
                entity.LastSavedDate = DateTime.UtcNow;
                var fields = string.Join(", ", typeof(T).GetProperties().Select(prop => $"[{prop.Name}]"));
                var values = string.Join(", ", typeof(T).GetProperties().Select(prop => $"@{prop.Name}"));

                await db.ExecuteAsync($"INSERT INTO {_table} ({fields}) VALUES ({values})", entities);
            }
        }
        public virtual async Task Update(T entity)
        {
            try
            {
                await using var db = await GetSqlConnection();

                entity.LastSavedDate = DateTime.UtcNow;

                var notUpdateFields = new[] { "Id", "CreatedDate", "CreatedBy", "IsDeleted" };
                var parameters = string.Join(", ",
                    typeof(T).GetProperties().Where(prop => !notUpdateFields.Contains(prop.Name))
                        .Select(prop => $"{prop.Name} = @{prop.Name}"));
                await db.ExecuteAsync($"UPDATE {_table} SET {parameters} WHERE [Id] = @Id", entity);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual async Task UpdateMany(IEnumerable<T> entities)
        {
            await using var db = await GetSqlConnection();

            foreach (var entity in entities)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                var notUpdateFields = new[] { "Id", "CreatedDate", "CreatedBy", "IsDeleted" };
                var parameters = string.Join(", ",
                    typeof(T).GetProperties().Where(prop => !notUpdateFields.Contains(prop.Name))
                        .Select(prop => $"{prop.Name} = @{prop.Name}"));

                await db.ExecuteAsync($"UPDATE {_table} SET {parameters} WHERE [Id] = @Id", entities);
            }
        }
        public virtual async Task Delete(Guid id)
        {
            await using var db = await GetSqlConnection();
            await db.ExecuteAsync($"UPDATE {_table} SET IsDeleted = true WHERE [Id] = @Id", new { Id = id });
        }
        public virtual async Task DeleteMany(IEnumerable<Guid> ids)
        {
            await using var db = await GetSqlConnection();
            foreach (var id in ids)
            {
                await db.ExecuteAsync($"UPDATE {_table} SET IsDeleted = 1 WHERE [Id] = @Id", new { Id = id });
            }
        }
        protected async Task<SqlConnection> GetSqlConnection()
        {
            var db = new SqlConnection(_connectionString);
                db.Open(); // <-- важно, иначе перед каждым запросом выполняется реконнект
            return db;
        }
    }
}