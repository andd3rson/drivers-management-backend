namespace Drivers_Management.Application.Dtos;

public class ApiResponse<T>
{
    public ApiResponse(T data)
    {
        Data = data;
        Errors = null;
    }
    public ApiResponse()
    { }
    public T? Data { get; set; }
    public string[]? Errors { get; set; }
}

