using Dapper;
using Microsoft.Data.SqlClient;
using WhoWantToBeAMillionaire_2._0.Entities;

public class QuestionRepository
{
    private static readonly string connectionString = "Server=localhost;Database=MillionaireGame;Integrated Security=True;TrustServerCertificate=True;";

    public QuestionRepository() { }

    public Question GetQuestionById(int id)
    {
        var sql = "SELECT * FROM Questions WHERE Id = @Id;";
        using (var connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<Question>(sql, new { Id = id });
        }
    }

    public Question GetRandomQuestionByDifficulty(int difficulty, IEnumerable<int> usedQuestionIds)
    {
        var sql = "SELECT TOP(1) * FROM Questions WHERE Difficulty = @Difficulty";
        if (usedQuestionIds != null && usedQuestionIds.Any())
        {
            sql += " AND Id NOT IN @UsedQuestionIds";
        }
        sql += " ORDER BY NEWID();";

        using (var connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<Question>(sql, new { Difficulty = difficulty, UsedQuestionIds = usedQuestionIds });
        }
    }
}