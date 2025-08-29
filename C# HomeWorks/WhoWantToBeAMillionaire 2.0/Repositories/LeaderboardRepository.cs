using Dapper;
using Microsoft.Data.SqlClient;
using WhoWantToBeAMillionaire_2._0.Entities;


public class LeaderboardRepository
{
    private static readonly string connectionString = "Server=localhost;Database=MillionaireGame;Integrated Security=True;TrustServerCertificate=True;";

    public LeaderboardRepository() { }

    public Leaderboard GetLeaderboardEntryByUserId(int userId)
    {
        var sql = "SELECT * FROM Leaderboard WHERE UserId = @UserId;";
        using (var connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<Leaderboard>(sql, new { UserId = userId });
        }
    }

    public void CreateLeaderboardEntry(Leaderboard entry)
    {
        var sql = @"
            INSERT INTO Leaderboard (UserId, FinalScore, GameEndTime)
            VALUES (@UserId, @FinalScore, @GameEndTime);";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, entry);
        }
    }

    public void UpdateLeaderboardEntry(Leaderboard entry)
    {
        var sql = @"
            UPDATE Leaderboard
            SET FinalScore = @FinalScore, GameEndTime = @GameEndTime
            WHERE UserId = @UserId;";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, entry);
        }
    }

    public IEnumerable<LeaderboardEntryDto> GetTopScoresWithLoginNames()
    {
        var sql = @"
            SELECT TOP 10
                u.LoginName,
                l.FinalScore,
                l.GameEndTime
            FROM Leaderboard AS l
            JOIN Users AS u ON l.UserId = u.Id
            ORDER BY l.FinalScore DESC, l.GameEndTime ASC;";

        using (var connection = new SqlConnection(connectionString))
        {
            return connection.Query<LeaderboardEntryDto>(sql);
        }
    }
}