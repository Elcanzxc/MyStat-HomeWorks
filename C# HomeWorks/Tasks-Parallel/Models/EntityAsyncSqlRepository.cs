using Async_Await.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserProfileApp.Repositories
{
    public class EntityAsyncSqlRepository
    {
        private static readonly string connectionString =
            "Server=localhost;Database=UserProfile;Integrated Security=True;TrustServerCertificate=True;";

        public EntityAsyncSqlRepository() { }

        public Task Create(User user)
        {
            return Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "INSERT INTO Users (Name, Surname, BirthDate) VALUES (@Name, @Surname, @BirthDate)";
                connection.Execute(sql, user);
            });
        }

        public Task<User?> GetById(int id)
        {
            return Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "SELECT * FROM Users WHERE Id = @Id";
                return connection.QueryFirstOrDefault<User>(sql, new { Id = id });
            });
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "SELECT * FROM Users";
                return connection.Query<User>(sql);
            });
        }

        public Task Update(User user)
        {
            return Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "UPDATE Users SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate WHERE Id = @Id";
                connection.Execute(sql, user);
            });
        }

        public Task Delete(int id)
        {
            return Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "DELETE FROM Users WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            });
        }

        public Task BulkInsertParallel(IEnumerable<User> users)
        {
            return Task.Run(() =>
            {
                Parallel.ForEach(users, user =>
                {
                    using var connection = new SqlConnection(connectionString);
                    connection.Open();
                    string sql = "INSERT INTO Users (Name, Surname, BirthDate) VALUES (@Name, @Surname, @BirthDate)";
                    connection.Execute(sql, user);
                });
            });
        }
    }
}
