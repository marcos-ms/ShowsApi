namespace SolucionApi.Shared;

public static class ErrorMessagesFilter
{
    public const string InvalidStatus = "Status must be 1, 2, or 3.";
    public const string InvalidMinAverage = "MinAverage must be between 0.0 and 10.0.";
    public const string InvalidMaxAverage = "MaxAverage must be between 0.0 and 10.0.";
    public const string MinGreaterThanMax = "MinAverage cannot be greater than MaxAverage.";
}