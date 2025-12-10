using System;

abstract class Goal
{
    // Encapsulation: private fields
    protected string _goalName;
    protected int _points;
    protected bool _isComplete;

    // Constructor
    public Goal(string goalName, int points)
    {
        _goalName = goalName;
        _points = points;
        _isComplete = false;
    }

    // Abstraction: shared behavior
    public string GetGoalName() => _goalName;
    public int GetPoints() => _points;
    public bool IsComplete() => _isComplete;

    // Polymorphism: abstract method for recording progress
    public abstract int RecordEvent();

    // Display goal status
    public abstract void DisplayGoal();
}
