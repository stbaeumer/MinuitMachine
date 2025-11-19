namespace MinuitMachine.Models;

public class SwaggerEndpoint
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Method { get; set; } = "GET";
    public string Description { get; set; } = string.Empty;
    public List<EndpointParameter> Parameters { get; set; } = new();
}

public class EndpointParameter
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "string";
    public bool Required { get; set; } = false;
    public string Description { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
