using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // --- Static fields accessible to all methods ---
    static List<Goal> goals = new List<Goal>();
    static int totalScore = 0;

    static void Main(string[] args)
    {
        // Load saved goals at startup
        LoadGoals();

        bool exit = false;
        while (!exit)
        {
            // --- Display Level, Title, Achievements ---
            Console.WriteLine($"\nLevel: {GetLevel()} - Title: {GetTitle()}");
            ShowAchievements();

            // --- Menu ---
            Console.WriteLine("\n--- Eternal Quest Menu ---");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save & Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": RecordEvent(); break;
                case "3": DisplayGoals(); break;
                case "4": Console.WriteLine($"Total Score: {totalScore}"); break;
                case "5": SaveGoals(); exit = true; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    // --- Gamification Methods ---
    static int GetLevel() => totalScore / 500 + 1;

    static string GetTitle()
    {
        if (totalScore >= 1500) return "Eternal Champion";
        if (totalScore >= 1000) return "Ninja Unicorn";
        if (totalScore >= 500) return "Novice Hero";
        return "Apprentice";
    }

    static void ShowAchievements()
    {
        int checklistCompleted = 0;
        foreach (var goal in goals)
            if (goal is ChecklistGoal cg && cg.IsGoalComplete) checklistCompleted++;

        Console.WriteLine("--- Achievements ---");
        if (checklistCompleted >= 1) Console.WriteLine("✔ First Checklist Completed!");
        if (checklistCompleted >= 5) Console.WriteLine("✔ Five Checklist Goals Completed!");
        if (totalScore >= 1000) Console.WriteLine("✔ 1000 Points Achieved!");
    }

    // --- Create a new goal ---
    static void CreateGoal()
    {
        Console.WriteLine("\nSelect Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, points));
                break;
            case "3":
                Console.Write("Enter number of completions required: ");
                int required = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, points, required, bonus));
                break;
            default:
                Console.WriteLine("Invalid type.");
                return;
        }

        Console.WriteLine("Goal created successfully!");
    }

    // --- Record an event for a goal ---
    static void RecordEvent()
    {
        DisplayGoals();
        Console.Write("Enter the number of the goal to record: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index))
        {
            index -= 1;
            if (index >= 0 && index < goals.Count)
            {
                int oldLevel = GetLevel();
                totalScore += goals[index].RecordEvent();
                int newLevel = GetLevel();
                if (newLevel > oldLevel)
                    Console.WriteLine($"🎉 Congratulations! You leveled up to Level {newLevel}!");
            }
            else
            {
                Console.WriteLine("Invalid goal number.");
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid number.");
        }
    }

    // --- Display all goals ---
    static void DisplayGoals()
    {
        Console.WriteLine("\n--- Goals ---");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            goals[i].DisplayGoal();
        }
    }

    // --- Save goals to a file ---
    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(totalScore);
            foreach (Goal goal in goals)
            {
                if (goal is SimpleGoal)
                    writer.WriteLine($"Simple|{goal.GetGoalName()}|{goal.GetPoints()}|{goal.IsComplete()}");
                else if (goal is EternalGoal)
                    writer.WriteLine($"Eternal|{goal.GetGoalName()}|{goal.GetPoints()}");
                else if (goal is ChecklistGoal cg)
                    writer.WriteLine($"Checklist|{cg.GetGoalName()}|{cg.GetPoints()}|{cg.IsGoalComplete}|{cg.CurrentCount}|{cg.BonusPoints}|{cg.RequiredCount}");
            }
        }
        Console.WriteLine("Goals saved successfully!");
    }

    // --- Load goals from a file ---
    static void LoadGoals()
    {
        if (!File.Exists("goals.txt")) return;

        string[] lines = File.ReadAllLines("goals.txt");
        totalScore = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            switch (parts[0])
            {
                case "Simple":
                    SimpleGoal sg = new SimpleGoal(parts[1], int.Parse(parts[2]));
                    if (bool.Parse(parts[3]))
                        sg.RecordEvent();
                    goals.Add(sg);
                    break;

                case "Eternal":
                    goals.Add(new EternalGoal(parts[1], int.Parse(parts[2])));
                    break;

                case "Checklist":
                    ChecklistGoal cg = new ChecklistGoal(
                        parts[1],                  // name
                        int.Parse(parts[2]),       // points
                        int.Parse(parts[6]),       // requiredCount
                        int.Parse(parts[5])        // bonusPoints
                    );
                    int currentCount = int.Parse(parts[4]);
                    bool isComplete = bool.Parse(parts[3]);
                    cg.RestoreState(currentCount, isComplete);
                    goals.Add(cg);
                    break;
            }
        }
    }
}
