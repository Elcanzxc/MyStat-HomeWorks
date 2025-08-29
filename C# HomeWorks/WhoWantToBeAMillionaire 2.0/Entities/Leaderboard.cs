

namespace WhoWantToBeAMillionaire_2._0.Entities;

public class Leaderboard
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal FinalScore { get; set; }
    public DateTime GameEndTime { get; set; }
}
