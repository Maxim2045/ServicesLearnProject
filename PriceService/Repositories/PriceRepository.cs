using System.Collections.Generic;
using System.Threading.Tasks;
using BaseRepositories;
using Dapper;
using Microsoft.Extensions.Options;
using PriceService.Repositories;

namespace PriceService.Repository
{
    public class PriceRepository : BaseRepository<PriceDbModel>, IPriceRepository
    {
        private static string Table => "Price";
        public PriceRepository(IOptions<PriceDbOptions> dbOptions) : base(dbOptions, Table)
        {
        }

        public override async Task<IEnumerable<PriceDbModel>> GetAll()
        {
            await using var db = await GetSqlConnection();
            return await db.QueryAsync<PriceDbModel>($"SELECT * FROM [Price] WHERE [IsDeleted] = 0");
        }
        /*
        public User Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age)";
                db.Execute(sqlQuery, user);

                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }
        
        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
                db.Execute(sqlQuery, user);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }*/
    }
}