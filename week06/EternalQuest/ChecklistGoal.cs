class ChecklistGoal : Goal
{
    private int _requiredCount;
    private int _currentCount;
    private int _bonusPoints;

    public int CurrentCount => _currentCount;
    public int BonusPoints => _bonusPoints;
    public int RequiredCount => _requiredCount;
    public bool IsGoalComplete
    {
        get { return _isComplete; }
    }

    public ChecklistGoal(string goalName, int points, int requiredCount, int bonusPoints)
        : base(goalName, points)
    {
        _requiredCount = requiredCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _currentCount++;
            int totalPoints = _points;

            if (_currentCount >= _requiredCount)
            {
                _isComplete = true;
                totalPoints += _bonusPoints;
                Console.WriteLine($"Checklist Goal '{_goalName}' completed! +{_points} points + bonus {_bonusPoints} points.");
            }
            else
            {
                Console.WriteLine($"Checklist Goal '{_goalName}' progress: {_currentCount}/{_requiredCount} +{_points} points.");
            }

            return totalPoints;
        }
        else
        {
            Console.WriteLine($"Checklist Goal '{_goalName}' already completed.");
            return 0;
        }
    }

    public override void DisplayGoal()
    {
        string status = _isComplete ? "[X]" : "[ ]";
        Console.WriteLine($"{status} {_goalName} ({_points} points each) Progress: {_currentCount}/{_requiredCount}");
    }

    // NEW METHOD: Restore state safely
    public void RestoreState(int currentCount, bool isComplete)
    {
        _currentCount = currentCount;
        _isComplete = isComplete;
    }
}
