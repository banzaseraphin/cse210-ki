using System;

// Orchestrator for the mindfulness app menu and activity selection
class MindfulnessApp
{
    private readonly BreathingActivity _breathing;
    private readonly ReflectionActivity _reflection;
    private readonly ListingActivity _listing;

    public MindfulnessApp()
    {
        _breathing = new BreathingActivity();
        _reflection = new ReflectionActivity();
        _listing = new ListingActivity();
    }

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("-------------------");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option (1-4): ");

            string choice = Console.ReadLine()?.Trim();
            switch (choice)
            {
                case "1":
                    _breathing.Run();
                    break;
                case "2":
                    _reflection.Run();
                    break;
                case "3":
                    _listing.Run();
                    break;
                case "4":
                    exit = true;
                    Console.WriteLine("Goodbye — keep practicing mindfulness!");
                    System.Threading.Thread.Sleep(900);
                    break;
                default:
                    Console.WriteLine("Invalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
