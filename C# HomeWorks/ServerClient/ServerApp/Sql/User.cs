
namespace ServerApp.Sql;

public class User
{
    public int Id { get; set; }
    public string Nickname { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
    public DateTime ConnectedAt { get; set; }
    public DateTime? DisconnectedAt { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
