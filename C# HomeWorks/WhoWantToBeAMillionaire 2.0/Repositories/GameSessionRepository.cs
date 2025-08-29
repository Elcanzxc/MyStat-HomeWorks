using Dapper;
using Microsoft.Data.SqlClient;
using WhoWantToBeAMillionaire_2._0.Entities;

public class GameSessionRepository
{
    private static readonly string connectionString = "Server=localhost;Database=MillionaireGame;Integrated Security=True;TrustServerCertificate=True;";

    public GameSessionRepository() { }

    public GameSession FindActiveSessionByUserId(int userId)
    {
        var sql = "SELECT * FROM GameSessions WHERE UserId = @UserId AND IsFinished = 0;";
        using (var connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<GameSession>(sql, new { UserId = userId });
        }
    }

    public int CreateGameSession(GameSession session)
    {
        var sql = @"
            INSERT INTO GameSessions (UserId, CurrentQuestionIndex, CurrentWinnings, UsedQuestionIds, IsFinished, StartTime)
            VALUES (@UserId, @CurrentQuestionIndex, @CurrentWinnings, @UsedQuestionIds, @IsFinished, @StartTime);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using (var connection = new SqlConnection(connectionString))
        {
            return connection.ExecuteScalar<int>(sql, session);
        }
    }

    public void UpdateGameSession(GameSession session)
    {
        var sql = @"
            UPDATE GameSessions
            SET
                CurrentQuestionIndex = @CurrentQuestionIndex,
                CurrentWinnings = @CurrentWinnings,
                UsedQuestionIds = @UsedQuestionIds,
                IsFinished = @IsFinished,
                EndTime = @EndTime
            WHERE Id = @Id;";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, session);
        }
    }
}