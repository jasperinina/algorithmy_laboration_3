using System.Diagnostics;

namespace lab3.Utilities;

public class TimeTracker
{
    private Stopwatch stopwatch;

    public TimeTracker()
    {
        stopwatch = new Stopwatch();
    }

    public void Start()
    {
        stopwatch.Restart();
    }

    public void Stop()
    {
        stopwatch.Stop();
    }

    public double GetElapsedMilliseconds()
    {
        return stopwatch.Elapsed.TotalMilliseconds;
    }

    public double GetElapsedSeconds()
    {
        return stopwatch.Elapsed.TotalSeconds;
    }
}