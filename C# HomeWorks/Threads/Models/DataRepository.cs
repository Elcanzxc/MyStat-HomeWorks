using Dapper;
using Microsoft.Data.SqlClient;

namespace Threads.Models;

public class DataRepository
{
    private readonly string connectionString;

    public DataRepository(string connectionStringStr)
    {
        connectionString = connectionStringStr;
    }

    public int EnsureUserExistsAndGetId(string username)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var existingUser = connection.QueryFirstOrDefault<dynamic>(
            "SELECT Id FROM UserMajorStatistic WHERE Username = @Username",
            new { Username = username }
        );

        if (existingUser != null)
        {
            connection.Execute(
                "UPDATE UserMajorStatistic SET LastSeenDateTime = @LastSeenDateTime WHERE Id = @Id",
                new { Id = existingUser.Id, LastSeenDateTime = DateTime.UtcNow }
            );
            return existingUser.Id;
        }
        else
        {
            return connection.ExecuteScalar<int>(
                "INSERT INTO UserMajorStatistic (Username, AllTime, LastSeenDateTime) VALUES (@Username, 0, @LastSeenDateTime); SELECT SCOPE_IDENTITY();",
                new { Username = username, LastSeenDateTime = DateTime.UtcNow }
            );
        }
    }
    public void UpdateUserTotalTime(int userId, int additionalTime)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Execute(
            "UPDATE UserMajorStatistic SET AllTime = AllTime + @AdditionalTime WHERE Id = @Id",
            new { Id = userId, AdditionalTime = additionalTime }
        );
    }
    public void UpsertPageStat(int userId, string pageName, int timeSpent)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var rowsAffected = connection.Execute(
            "UPDATE PageStats SET AllTime = AllTime + @TimeSpent WHERE UserMajorId = @UserId AND PageName = @PageName",
            new { UserId = userId, PageName = pageName, TimeSpent = timeSpent }
        );
    
        if (rowsAffected == 0)
        {
            connection.Execute(
                "INSERT INTO PageStats (Username, PageName, AllTime, UserMajorId) VALUES ((SELECT Username FROM UserMajorStatistic WHERE Id = @UserId), @PageName, @TimeSpent, @UserId);",
                new { UserId = userId, PageName = pageName, TimeSpent = timeSpent }
            );
        }
    }

    public void UpsertButtonClick(int userId, string buttonName)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var rowsAffected = connection.Execute(
            "UPDATE ButtonClicks SET ClickCount = ClickCount + 1 WHERE UserMajorId = @UserId AND Button = @ButtonName",
            new { UserId = userId, ButtonName = buttonName }
        );

        if (rowsAffected == 0)
        {
            connection.Execute(
                "INSERT INTO ButtonClicks (Username, Button, ClickCount, UserMajorId) VALUES ((SELECT Username FROM UserMajorStatistic WHERE Id = @UserId), @ButtonName, 1, @UserId);",
                new { UserId = userId, ButtonName = buttonName }
            );
        }
    }

    public int GetTotalTimeFromPages(int userId)
    {
        using var connection = new SqlConnection(connectionString);
        return connection.ExecuteScalar<int>(
            "SELECT SUM(AllTime) FROM PageStats WHERE UserMajorId = @UserId",
            new { UserId = userId }
        );
    }

    public void UpdateUserTotalTimeAbsolute(int userId, int totalTime)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Execute(
            "UPDATE UserMajorStatistic SET AllTime = @TotalTime WHERE Id = @Id",
            new { Id = userId, TotalTime = totalTime }
        );
    }
}