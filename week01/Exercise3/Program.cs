using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise3 Project.");
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;

        while (guess != magicNumber)
        {
            Console.Write("Guess a number between 1 and 101:");
            guess = int.Parse(Console.ReadLine());

            if (magicNumber > guess)
            {
                Console.WriteLine("Too low!");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("Too high!");
            }
            else
            {
                Console.WriteLine("Congratulations! You guessed the number!");
            }
        }
    }
}