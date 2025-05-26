namespace Presentation.Models;

public class PackageResult
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
}

public class PackageResult<T> : PackageResult
{
    public T? Result { get; set; }
}
