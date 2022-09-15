namespace MySpot.Api.ValueObjects;

public sealed record Week
{
    public Date From { get; }
    public Date To { get; }

    public Week(DateTimeOffset value)
    {
        var dayOfWeekNumber = (int) value.DayOfWeek;
        var pastDays = -1 * dayOfWeekNumber;
        var reamainingDays = 7 + pastDays;
        From = new Date(value.AddDays(pastDays));
        To = new Date(value.AddDays(reamainingDays));
    }

    public override string ToString() => $"{From} --> {To}";
}
