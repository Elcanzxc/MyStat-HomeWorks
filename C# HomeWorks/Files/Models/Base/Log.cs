
using Files.Models.Enum;

namespace Files.Models.Base;
public class Log
{
    public LOG_TYPE Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Exception? Exception { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
