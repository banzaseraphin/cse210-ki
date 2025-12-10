using System;

class Swimming : Activity
{
    private int _laps;       // number of laps
    private double _lapLength; // length of one lap in meters

    public Swimming(DateTime date, double length, int laps, double lapLength) : base(date, length)
    {
        _laps = laps;
        _lapLength = lapLength;
    }

    public override double GetDistance() => (_laps * _lapLength) / 1000.0; // meters to km

    public override double GetSpeed() => GetDistance() / (Length / 60); // km/h

    public override double GetPace() => Length / GetDistance(); // min/km
}
