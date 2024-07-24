namespace SolucionApi.Dtos;

public class ScheduleDto
{
    public string? Time { get; set; }
    public List<string> Days { get; set; } = new();
}