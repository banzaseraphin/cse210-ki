using System;

class SimpleGoal : Goal
{
    public SimpleGoal(string goalName, int points) : base(goalName, points) { }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"Simple Goal '{_goalName}' completed! +{_points} points.");
            return _points;
        }
        else
        {
            Console.WriteLine($"Simple Goal '{_goalName}' already completed.");
            return 0;
        }
    }

    public override void DisplayGoal()
    {
        string status = _isComplete ? "[X]" : "[ ]";
        Console.WriteLine($"{status} {_goalName} ({_points} points)");
    }
}
