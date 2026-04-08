using System.Text.Json;
using BlazorApp.ApiRequest.Model;

namespace BlazorApp.ApiRequest;

public class RequestApi
{
    private readonly HttpClient _httpClient;

    public RequestApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ImageData> GetAllImagesAsync()
    {
        var url = "/GetAllImages";

        try
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(responseContent))
            {
                return new ImageData();
            }

            var userData = JsonSerializer.Deserialize<ImageData>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            
            return userData ?? new ImageData();
        }
        catch (Exception ex)
        {
            return new ImageData();
        }
    }
}