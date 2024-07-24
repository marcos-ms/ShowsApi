namespace SolucionApi.Data.Entities;

public class ScheduleEntity
{
    public string? Time { get; set; }
    public List<string> Days { get; set; } = new();
}