using System;
using System.IO;
using System.Collections.Generic;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    private List<string> _prompts = new List<string>()
    {
        "Who was the most interesting person you interacted with today?",
        "What was the best part of your day?",
        "How did you see the hand of the Lord in your life today?",
        "What was the strongest emotion you felt today?",
        "If you could do one thing over today, what would it be?",
        // You can add more prompts if you want
    };

    public void WriteNewEntry()
    {
        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry newEntry = new Entry();
        newEntry._date = DateTime.Now.ToShortDateString();
        newEntry._prompt = prompt;
        newEntry._response = response;

        _entries.Add(newEntry);

        Console.WriteLine("\nEntry added successfully!\n");
    }

    public void DisplayJournal()
    {
        Console.WriteLine("\n--- Journal Entries ---\n");

        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.\n");
            return;
        }

        foreach (Entry e in _entries)
        {
            e.DisplayEntry();
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            foreach (Entry e in _entries)
            {
                outputFile.WriteLine($"{e._date}|{e._prompt}|{e._response}");
            }
        }

        Console.WriteLine("\nJournal saved successfully!\n");
    }

    public void LoadFromFile(string fileName)
    {
        _entries.Clear();

        string[] lines = File.ReadAllLines(fileName);

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");

            Entry e = new Entry();
            e._date = parts[0];
            e._prompt = parts[1];
            e._response = parts[2];

            _entries.Add(e);
        }

        Console.WriteLine("\nJournal loaded successfully!\n");
    }
}
