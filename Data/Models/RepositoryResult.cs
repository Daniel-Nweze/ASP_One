namespace Data.Models;

public class RepositoryResult<T>
{
    public bool Succeeded { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
    public T? Data { get; set; } 
}


