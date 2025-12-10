using System;

abstract class Activity
{
    // Encapsulated member variables
    private DateTime _date;
    private double _length; // length in minutes

    // Constructor
    public Activity(DateTime date, double length)
    {
        _date = date;
        _length = length;
    }

    // Properties to access private fields
    public DateTime Date => _date;
    public double Length => _length;

    // Abstract methods for polymorphism
    public abstract double GetDistance(); // in km
    public abstract double GetSpeed();    // km/h
    public abstract double GetPace();     // min/km

    // Virtual method using polymorphism
    public virtual string GetSummary()
    {
        return $"{_date.ToShortDateString()} - {this.GetType().Name}: " +
               $"Distance: {GetDistance():F2} km, Speed: {GetSpeed():F2} km/h, Pace: {GetPace():F2} min/km";
    }
}
