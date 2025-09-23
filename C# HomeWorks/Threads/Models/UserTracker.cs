
namespace Threads.Models;

public class UserTracker
{
    private string Username { get; set; }
    private int UserMajorId { get; set; }
    private DataRepository DataRepository { get; set; }

    private readonly CancellationTokenSource cts = new();
    private readonly Dictionary<string, DateTime> pageTimers = new();

    private Thread? backgroundThread;
    private DateTime sessionStartTime;
    private DateTime lastUpdateTime;
    private readonly object lockObject = new object();

    public UserTracker(string username, int userMajorId, DataRepository dataRepository)
    {
        Username = username;
        UserMajorId = userMajorId;
        DataRepository = dataRepository;
    }

    public void StartTracking()
    {
        sessionStartTime = DateTime.UtcNow;
        lastUpdateTime = sessionStartTime;

        backgroundThread = new Thread(() =>
        {
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(30));
                    lock (lockObject)
                    {
                        var now = DateTime.UtcNow;
                        var delta = (int)(now - lastUpdateTime).TotalSeconds;

                        if (delta > 0)
                        {
                            DataRepository.UpdateUserTotalTime(UserMajorId, delta);
                            lastUpdateTime = now;
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
            }
        });
        backgroundThread.IsBackground = true;
        backgroundThread.Start();
    }

    public void StopTrackingAndSave()
    {
        cts.Cancel();
        backgroundThread?.Join();

        lock (lockObject)
        {
            foreach (var kvp in pageTimers)
            {
                var timeSpent = (int)(DateTime.UtcNow - kvp.Value).TotalSeconds;
                if (timeSpent > 0)
                {
                    DataRepository.UpsertPageStat(UserMajorId, kvp.Key, timeSpent);
                }
            }
            pageTimers.Clear();

            var totalTime = DataRepository.GetTotalTimeFromPages(UserMajorId);
            DataRepository.UpdateUserTotalTimeAbsolute(UserMajorId, totalTime);
        }
    }

    public void TrackPageEntry(string pageName)
    {
        lock (lockObject)
        {
            if (!pageTimers.ContainsKey(pageName))
            {
                pageTimers[pageName] = DateTime.UtcNow;
            }
        }
    }

    public void TrackPageExit(string pageName)
    {
        lock (lockObject)
        {
            if (pageTimers.TryGetValue(pageName, out DateTime entryTime))
            {
                var timeSpent = (int)(DateTime.UtcNow - entryTime).TotalSeconds;
                if (timeSpent > 0)
                {
                    DataRepository.UpsertPageStat(UserMajorId, pageName, timeSpent);
                }
                pageTimers.Remove(pageName);
            }
        }
    }

    public void TrackButtonClick(string buttonName)
    {
        DataRepository.UpsertButtonClick(UserMajorId, buttonName);
    }
}
