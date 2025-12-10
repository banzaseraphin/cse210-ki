using System;

class Cycling : Activity
{
    private double _speed; // average speed in km/h

    public Cycling(DateTime date, double length, double speed) : base(date, length)
    {
        _speed = speed;
    }

    public override double GetDistance() => _speed * (Length / 60); // distance = speed * time

    public override double GetSpeed() => _speed;

    public override double GetPace() => 60 / _speed; // min/km
}
