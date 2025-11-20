using System.Net.Http.Headers;

namespace MinuitMachine.Client.Services;

public class ApiClient
{
    private readonly CredentialStore _credentials;
    private readonly HttpClient _http;

    public ApiClient(CredentialStore credentials)
    {
        _credentials = credentials;
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    public async Task<(string status, string body)> GetTextAsync(string url)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        var auth = _credentials.GetBasicAuthHeader();
        if (!string.IsNullOrEmpty(auth))
        {
            req.Headers.Authorization = AuthenticationHeaderValue.Parse(auth);
        }
        using var res = await _http.SendAsync(req);
        var text = await res.Content.ReadAsStringAsync();
        return ($"{(int)res.StatusCode} {res.ReasonPhrase}", text);
    }
}
