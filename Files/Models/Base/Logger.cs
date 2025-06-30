using Files.Models.Interfaces;
namespace Files.Models.Base;

public class Logger : ILogger
{
 
    private string filePath = "logs.txt";
    public void Write(Log log)
    {
        string logText = BuildLogText(log);

       
        File.AppendAllText(filePath, logText);
    }

    private string BuildLogText(Log log)
    {

        string exceptionText = log.Exception == null ? "" : $"Exception: {log.Exception.GetType().Name}\n";

        return
            $"=== Создание LOG ===\n" +
            $"[{log.CreationDate}] [{log.Type}]\n" +
            $"Title: {log.Title}\n" +
            $"Description: {log.Description}\n" +
            exceptionText +
            $"======================";
    }
}
