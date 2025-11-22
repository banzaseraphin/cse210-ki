using System;

class Program
{
    static void Main()
    {
        // Creativity / Exceeding Core Requirements:
        // 1. Only hiding words that are not already hidden.
        // 2. Multiple scriptures could be added as a future library.
        // 3. Random hiding amount could be adjusted for difficulty.
        Reference reference = new Reference("John", 3, 16);
        Scripture scripture = new Scripture(reference,
            "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        while (true)
        {
            Console.Clear();
            scripture.Display();

            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("All words are hidden. Program ended.");
                break;
            }

            Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break;

            // Hide 2-3 words randomly
            scripture.HideRandomWords(3);
        }
    }
}
