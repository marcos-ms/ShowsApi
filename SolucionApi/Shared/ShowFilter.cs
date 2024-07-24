namespace SolucionApi.Shared;

public class ShowFilter
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Genres { get; set; }
    public int? Status { get; set; }
    public double? MinAverage { get; set; }
    public double? MaxAverage { get; set; }

    public bool IsValid(out string errorMessage)
    {
        errorMessage = string.Empty;

        if (Status.HasValue && (Status < 1 || Status > 3))
        {
            errorMessage = ErrorMessagesFilter.InvalidStatus;
            return false;
        }

        if (MinAverage.HasValue && (MinAverage < 0.0 || MinAverage > 10.0))
        {
            errorMessage = ErrorMessagesFilter.InvalidMinAverage;
            return false;
        }

        if (MaxAverage.HasValue && (MaxAverage < 0.0 || MaxAverage > 10.0))
        {
            errorMessage = ErrorMessagesFilter.InvalidMaxAverage;
            return false;
        }

        if (MinAverage.HasValue && MaxAverage.HasValue && MinAverage > MaxAverage)
        {
            errorMessage = ErrorMessagesFilter.MinGreaterThanMax;
            return false;
        }

        return true;
    }
}
