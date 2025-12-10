using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        // Add sample activities
        activities.Add(new Running(DateTime.Now, 30, 5));       // 30 min, 5 km
        activities.Add(new Cycling(DateTime.Now, 60, 20));      // 60 min, 20 km/h
        activities.Add(new Swimming(DateTime.Now, 45, 30, 50)); // 45 min, 30 laps, 50m each

        // Display summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
