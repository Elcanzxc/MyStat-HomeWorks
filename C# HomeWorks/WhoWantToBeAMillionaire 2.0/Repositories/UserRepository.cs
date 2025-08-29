using Dapper;
using Microsoft.Data.SqlClient;

using WhoWantToBeAMillionaire_2._0.Entities;

public class UserRepository
{
    private static readonly string connectionString = "Server=localhost;Database=MillionaireGame;Integrated Security=True;TrustServerCertificate=True;";

    public UserRepository()
    {
    }

    public User GetUserByLoginName(string loginName)
    {
        var sql = "SELECT * FROM Users WHERE LoginName = @LoginName";

        using (var connection = new SqlConnection(connectionString))
        {

            return connection.Query<User>(sql, new { LoginName = loginName }).FirstOrDefault();
        }
    }

    public void CreateUser(User user)
    {
        var sql = "INSERT INTO Users (LoginName) VALUES (@LoginName);";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, user);
        }
    }

    public User GetUserById(int id)
    {
        var sql = "SELECT * FROM Users WHERE Id = @Id;";
        using (var connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<User>(sql, new { Id = id });
        }
    }


}