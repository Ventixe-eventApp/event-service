namespace Presentation.Models;

public class EventResult
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
}

public class EventResult<T> : EventResult
{
    public T? Result { get; set; }
}
