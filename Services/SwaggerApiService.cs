using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MinuitMachine.Models;

namespace MinuitMachine.Services;

public class SwaggerApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public SwaggerApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    public async Task<ApiResponse> FetchSwaggerDocumentAsync(string swaggerUrl)
    {
        try
        {
            var response = await _httpClient.GetAsync(swaggerUrl);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return new ApiResponse
                {
                    Success = true,
                    Data = content,
                    StatusCode = (int)response.StatusCode
                };
            }
            else
            {
                return new ApiResponse
                {
                    Success = false,
                    ErrorMessage = $"Failed to fetch Swagger document: {response.StatusCode}",
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                Success = false,
                ErrorMessage = $"Error fetching Swagger document: {ex.Message}",
                StatusCode = 0
            };
        }
    }

    public async Task<ApiResponse> ExecuteApiCallAsync(ApiConfiguration config, SwaggerEndpoint endpoint)
    {
        try
        {
            var url = BuildUrl(config.BaseUrl, endpoint);
            
            // Add API key header if provided
            if (!string.IsNullOrEmpty(config.ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.ApiKey}");
            }

            HttpResponseMessage response;

            switch (endpoint.Method.ToUpper())
            {
                case "GET":
                    response = await _httpClient.GetAsync(url);
                    break;
                    
                case "POST":
                    var postContent = BuildRequestContent(endpoint);
                    response = await _httpClient.PostAsync(url, postContent);
                    break;
                    
                case "PUT":
                    var putContent = BuildRequestContent(endpoint);
                    response = await _httpClient.PutAsync(url, putContent);
                    break;
                    
                case "DELETE":
                    response = await _httpClient.DeleteAsync(url);
                    break;
                    
                default:
                    return new ApiResponse
                    {
                        Success = false,
                        ErrorMessage = $"Unsupported HTTP method: {endpoint.Method}",
                        StatusCode = 0
                    };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                Data = responseContent,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? null : $"API call failed with status {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                Success = false,
                ErrorMessage = $"Error executing API call: {ex.Message}",
                StatusCode = 0
            };
        }
    }

    private string BuildUrl(string baseUrl, SwaggerEndpoint endpoint)
    {
        var url = baseUrl.TrimEnd('/') + '/' + endpoint.Path.TrimStart('/');
        
        // Add query parameters for GET requests
        if (endpoint.Method.ToUpper() == "GET" && endpoint.Parameters.Any())
        {
            var queryParams = endpoint.Parameters
                .Where(p => !string.IsNullOrEmpty(p.Value))
                .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.Value)}");
            
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }
        }
        
        return url;
    }

    private StringContent BuildRequestContent(SwaggerEndpoint endpoint)
    {
        var bodyParams = endpoint.Parameters
            .Where(p => !string.IsNullOrEmpty(p.Value))
            .ToDictionary(p => p.Name, p => p.Value);
        
        var json = JsonSerializer.Serialize(bodyParams, _jsonOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
