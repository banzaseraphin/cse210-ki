using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

/// <summary>
/// Base Activity class: contains shared attributes and behaviors.
/// - activityName, description and duration are stored here (in private fields)
/// - shared methods: Run (template), Start/Finish messages, spinner and countdown
/// - Derived classes implement RunActivityBody(int durationSeconds)
/// </summary>
abstract class Activity
{
    // Private member variables (encapsulation requirement)
    private readonly string _activityName;
    private readonly string _description;
    private int _durationSeconds;

    private static readonly Random _rng = new Random();

    protected Activity(string activityName, string description)
    {
        _activityName = activityName;
        _description = description;
    }

    // Template method
    public void Run()
    {
        Console.Clear();
        Console.WriteLine($"Activity: {_activityName}\n");
        Console.WriteLine(_description);

        _durationSeconds = PromptForDuration();
        ShowStartingMessage();
        PrepareToBegin();
        RunActivityBody(_durationSeconds);
        ShowEndingMessage();
        LogCompletion();
    }

    // Derived classes supply the core behavior
    protected abstract void RunActivityBody(int durationSeconds);

    private int PromptForDuration()
    {
        while (true)
        {
            Console.Write("\nEnter duration in seconds: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int secs) && secs > 0)
            {
                return secs;
            }
            Console.WriteLine("Please enter a positive integer for seconds.");
        }
    }

    private void ShowStartingMessage()
    {
        Console.WriteLine("\nGet ready...");
        Console.WriteLine($"You have chosen {_activityName} for {_durationSeconds} seconds.");
    }

    protected void PrepareToBegin()
    {
        Console.Write("\nPreparing");
        ShowSpinner(3);
        Console.WriteLine("\nBegin!");
    }

    private void ShowEndingMessage()
    {
        Console.WriteLine("\nWell done!");
        Console.WriteLine($"You have completed the {_activityName} for {_durationSeconds} seconds.");
        Console.Write("Finishing");
        ShowSpinner(3);
        Console.WriteLine("\nPress Enter to return to the menu.");
        Console.ReadLine();
    }

    // Spinner animation using backspace
    protected void ShowSpinner(int seconds)
    {
        char[] seq = new char[] { '|', '/', '-', '\\' };
        int idx = 0;
        Stopwatch sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < seconds * 1000)
        {
            Console.Write(seq[idx++ % seq.Length]);
            Thread.Sleep(200);
            Console.Write("\b");
        }
    }

    // Countdown that prints numbers and erases them (backspace)
    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    // Simple logging to a local file (exceeds requirements)
    private void LogCompletion()
    {
        try
        {
            string logLine = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC - Completed {_activityName} - Duration: {_durationSeconds}s";
            File.AppendAllLines("activity_log.txt", new[] { logLine });
        }
        catch
        {
            // Ignore logging errors
        }
    }

    // Utility for derived classes: random integer
    protected int RandomInt(int minInclusive, int maxExclusive)
    {
        lock (_rng)
        {
            return _rng.Next(minInclusive, maxExclusive);
        }
    }
}
