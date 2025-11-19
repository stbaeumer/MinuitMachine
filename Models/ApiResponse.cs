namespace MinuitMachine.Models;

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }
}
