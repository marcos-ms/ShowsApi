namespace SolucionApi.Shared;

public class ErrorResponse
{
    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public ErrorResponse(int statusCode, string message, string details) : this(statusCode, message)
    {
        Details = details;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}