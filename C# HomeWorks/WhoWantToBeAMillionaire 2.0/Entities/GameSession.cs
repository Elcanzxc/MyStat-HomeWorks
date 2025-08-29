
namespace WhoWantToBeAMillionaire_2._0.Entities;

public class GameSession
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CurrentQuestionIndex { get; set; }
    public decimal CurrentWinnings { get; set; }
    public string UsedQuestionIds { get; set; } 
    public bool IsFinished { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; } 
}
