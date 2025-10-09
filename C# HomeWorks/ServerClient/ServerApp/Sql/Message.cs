

namespace ServerApp.Sql;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime SentAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
