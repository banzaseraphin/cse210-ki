using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Reflection Activity: selects a prompt and then shows reflection questions.
/// Questions are presented without repeat until the full list has been used in the session (exceeds requirement).
/// </summary>
class ReflectionActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private Queue<string> _promptQueue;
    private Queue<string> _questionQueue;

    public ReflectionActivity()
        : base("Reflection Activity",
               "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _promptQueue = new Queue<string>(Shuffle(_prompts));
        _questionQueue = new Queue<string>(Shuffle(_questions));
    }

    protected override void RunActivityBody(int durationSeconds)
    {
        if (_promptQueue.Count == 0) _promptQueue = new Queue<string>(Shuffle(_prompts));
        string prompt = _promptQueue.Dequeue();

        Console.WriteLine("\nPrompt:");
        Console.WriteLine(prompt);
        Console.WriteLine("\nReflect on the following questions:");

        Stopwatch sw = Stopwatch.StartNew();

        while (sw.ElapsedMilliseconds < durationSeconds * 1000)
        {
            if (_questionQueue.Count == 0) _questionQueue = new Queue<string>(Shuffle(_questions));
            string question = _questionQueue.Dequeue();

            Console.WriteLine("\n- " + question);

            int pause = 6; // seconds per question
            long timeLeftMs = durationSeconds * 1000 - sw.ElapsedMilliseconds;
            if (timeLeftMs <= 0) break;
            int pauseToUse = (int)Math.Min(pause, Math.Ceiling(timeLeftMs / 1000.0));
            ShowSpinner(pauseToUse);
        }

        Console.WriteLine();
    }

    // Fisher-Yates shuffle
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
