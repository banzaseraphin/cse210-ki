using System;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// Breathing Activity: alternates "Breathe in..." and "Breathe out..."
/// Uses countdowns for each inhale/exhale. Continues until total duration is reached.
/// </summary>
class BreathingActivity : Activity
{
    private readonly int _inhaleSeconds = 4;
    private readonly int _exhaleSeconds = 4;

    public BreathingActivity()
        : base("Breathing Activity",
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void RunActivityBody(int durationSeconds)
    {
        Stopwatch sw = Stopwatch.StartNew();
        bool inhaleNext = true;
        while (sw.ElapsedMilliseconds < durationSeconds * 1000)
        {
            if (inhaleNext)
            {
                Console.Write("\nBreathe in... ");
                ShowCountdown(_inhaleSeconds);
            }
            else
            {
                Console.Write("\nBreathe out... ");
                ShowCountdown(_exhaleSeconds);
            }
            inhaleNext = !inhaleNext;
        }
        Console.WriteLine();
    }
}
