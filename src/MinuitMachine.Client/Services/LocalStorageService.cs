using Microsoft.JSInterop;

namespace MinuitMachine.Client.Services;

public class LocalStorageService
{
    private readonly IJSRuntime _js;
    public LocalStorageService(IJSRuntime js) => _js = js;

    public ValueTask SetItemAsync(string key, string value)
        => _js.InvokeVoidAsync("localStorage.setItem", key, value);

    public async Task<string?> GetItemAsync(string key)
    {
        try
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", key);
        }
        catch
        {
            return null;
        }
    }

    public ValueTask RemoveItemAsync(string key)
        => _js.InvokeVoidAsync("localStorage.removeItem", key);
}
