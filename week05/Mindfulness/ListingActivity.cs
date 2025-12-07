using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// Listing Activity: shows a prompt, gives a short countdown, and lets the user list items until time expires.
/// The count of items is shown at the end.
/// </summary>
class ListingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private Queue<string> _promptQueue;

    public ListingActivity()
        : base("Listing Activity",
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _promptQueue = new Queue<string>(Shuffle(_prompts));
    }

    protected override void RunActivityBody(int durationSeconds)
    {
        if (_promptQueue.Count == 0) _promptQueue = new Queue<string>(Shuffle(_prompts));
        string prompt = _promptQueue.Dequeue();

        Console.WriteLine("\nPrompt:");
        Console.WriteLine(prompt);

        Console.WriteLine("\nYou will have a few seconds to think...");
        ShowCountdown(5);

        Console.WriteLine("\nStart listing items. Press Enter after each item.");

        List<string> items = new List<string>();
        Stopwatch sw = Stopwatch.StartNew();

        while (sw.ElapsedMilliseconds < durationSeconds * 1000)
        {
            int secondsLeft = (int)Math.Ceiling((durationSeconds * 1000 - sw.ElapsedMilliseconds) / 1000.0);
            Console.Write($"\nTime left: {secondsLeft}s - Item: ");
            string? entry = ReadLineWithTimeout(secondsLeft * 1000);
            if (entry == null)
            {
                Console.WriteLine("\nTime is up!");
                break;
            }
            entry = entry.Trim();
            if (!string.IsNullOrEmpty(entry)) items.Add(entry);
        }

        Console.WriteLine($"\nYou listed {items.Count} item(s).");
        if (items.Count > 0)
        {
            Console.WriteLine("Items:");
            foreach (var it in items) Console.WriteLine("- " + it);
        }
    }

    private static string? ReadLineWithTimeout(int timeoutMs)
    {
        string? result = null;
        Thread inputThread = new Thread(() =>
        {
            try
            {
                result = Console.ReadLine();
            }
            catch { }
        });
        inputThread.IsBackground = true;
        inputThread.Start();

        bool finished = inputThread.Join(timeoutMs);
        if (!finished)
        {
            return null; // timeout
        }
        return result;
    }

    private static List<T> Shuffle<T>(List<T> input)
    {
        var copy = new List<T>(input);
        var rng = new Random();
        for (int i = copy.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            T tmp = copy[i];
            copy[i] = copy[j];
            copy[j] = tmp;
        }
        return copy;
    }
}
