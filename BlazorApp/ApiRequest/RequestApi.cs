using System.Net.Http.Json;
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
    
    public async Task<ImageAddData> UploadImageAsync(Stream fileStream, string fileName)
    {
        var url = "/ProcessImage";
        
        try
        {
            using var content = new MultipartFormDataContent();
            
            var streamContent = new StreamContent(fileStream);
            content.Add(streamContent, "file", fileName);

            var response = await _httpClient.PostAsync(url, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            var imageData = JsonSerializer.Deserialize<ImageAddData>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            
            return imageData ?? new ImageAddData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            return new ImageAddData(); 
        }
    }
    
    public async Task<UserAddData> PostUserAsync(UserAddData user)
    {
        var url = "/PostUserRole";
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, user).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var userData = JsonSerializer.Deserialize<UserAddData>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            
            return userData ?? new UserAddData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new UserAddData();
        }
    }
}