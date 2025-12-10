using System;

class EternalGoal : Goal
{
    public EternalGoal(string goalName, int points) : base(goalName, points) { }

    public override int RecordEvent()
    {
        Console.WriteLine($"Eternal Goal '{_goalName}' recorded! +{_points} points.");
        return _points;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[∞] {_goalName} ({_points} points each time)");
    }
}
