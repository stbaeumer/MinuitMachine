namespace MinuitMachine.Client.Services;

public class CredentialStore
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? GetBasicAuthHeader()
    {
        if (string.IsNullOrEmpty(Username)) return null;
        var raw = System.Text.Encoding.UTF8.GetBytes($"{Username}:{Password}");
        return "Basic " + Convert.ToBase64String(raw);
    }
}
