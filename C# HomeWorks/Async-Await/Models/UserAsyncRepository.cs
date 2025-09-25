using Async_Await.Entities;
using Dapper;
using Microsoft.Data.SqlClient;


namespace UserProfileApp.Repositories;

public class UserAsyncRepository
{
    private static readonly string
        connectionString = "Server=localhost;Database=UserProfile;Integrated Security=True;TrustServerCertificate=True;";

    public UserAsyncRepository() {}

    public async Task CreateAsync(User user)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = @"INSERT INTO Users ([Name],[Surname],[BirthDate]) 
                           VALUES (@Name,@Surname,@BirthDate);";
        await connection.ExecuteAsync(sql, user);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = "SELECT * FROM Users WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = "SELECT * FROM Users";
        return await connection.QueryAsync<User>(sql);
    }

    public async Task UpdateAsync(User user)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = @"UPDATE Users 
                           SET [Name] = @Name, [Surname] = @Surname, [BirthDate] = @BirthDate
                           WHERE Id = @Id";
        await connection.ExecuteAsync(sql, user);
    }

    public async Task DeleteAsync(int id)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = "DELETE FROM Users WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
