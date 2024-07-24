namespace SolucionApi.Shared;

public class ResultValue
{
    public ResultValue()
    {
        
    }

    public ResultValue(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}

public class ResultValue<T> where T : class, new()
{
    public ResultValue()
    {
        
    }

    public ResultValue(bool success, string message, T? data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; } = null!;
}