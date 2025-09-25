using Async_Await.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Tasks_Parallel.Models
{
    public class EntitySyncSqlRepository
    {
        private static readonly string
            connectionString = "Server=localhost;Database=UserProfile;Integrated Security=True;TrustServerCertificate=True;";

        public EntitySyncSqlRepository(){}

        public void Create(User user)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "INSERT INTO Users (Name, Surname, BirthDate) VALUES (@Name, @Surname, @BirthDate)";
            connection.Execute(sql, user);
        }

        public User? GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Users WHERE Id = @Id";
            return connection.QueryFirstOrDefault<User>(sql, new { Id = id });
        }

        public IEnumerable<User> GetAll()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Users";
            return connection.Query<User>(sql);
        }

        public void Update(User user)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "UPDATE Users SET Name = @Name, Surname = @Surname, BirthDate = @BirthDate WHERE Id = @Id";
            connection.Execute(sql, user);
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "DELETE FROM Users WHERE Id = @Id";
            connection.Execute(sql, new { Id = id });
        }

        public void BulkInsert(IEnumerable<User> users)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "INSERT INTO Users (Name, Surname, BirthDate) VALUES (@Name, @Surname, @BirthDate)";
            connection.Execute(sql, users);
        }
    }
}
