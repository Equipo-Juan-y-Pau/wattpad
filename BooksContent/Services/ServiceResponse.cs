using BooksContent.Models;

public class ServiceResponse<T>
{
    public Book? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}