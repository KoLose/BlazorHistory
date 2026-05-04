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
    
    public async Task<ImageData> GetAllImagesAsync(Guid userId)
    {
        var url = $"/GetAllImages?userId={userId}";

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
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new ImageData();
        }
    }
    
    public async Task<ImageAddData> TestPostRequest(Stream fileStream, string fileName, Guid userId)
    {
        var url = "PostTestRequest"; 
    
        try
        {
            using var content = new MultipartFormDataContent();
        
            // Read format
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            var mimeType = ext switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
            
            // Open stream and record image
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);
            
            // Add file
            content.Add(streamContent, "FileUser", fileName); 

            // Add UserId
            content.Add(new StringContent(userId.ToString()), "UserId");

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
        var url = "/PostUser";
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
    
    public async Task<UserUpdateData> PatchUserAsync(UserDataShort updateUser)
    {
        var url = "/PatchUserRole";
        try
        {
            var response = await _httpClient.PatchAsJsonAsync(url, updateUser).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var userData = JsonSerializer.Deserialize<UserUpdateData>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            
            return userData ?? new UserUpdateData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new UserUpdateData();
        }
    }
    
    public async Task<UserUpdateAva> UploadAvatarAsync(Stream fileStream, string fileName, Guid userId)
    {
        var url = "PatchUserImage";

        try
        {
            using var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            ms.Position = 0;

            using var content = new MultipartFormDataContent();

            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            var mimeType = ext switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            var streamContent = new StreamContent(ms);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);

            content.Add(streamContent, "AvatarFile", fileName);
            content.Add(new StringContent(userId.ToString()), "UserId");

            var response = await _httpClient.PatchAsync(url, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<UserUpdateAva>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return result ?? new UserUpdateAva();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки аватара: {ex.Message}");
            return new UserUpdateAva();
        }
    }
    
    public async Task<UserData> GetAllUsersAsync()
    {
        var url = "/GetUser";

        try
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(responseContent))
            {
                return new UserData();
            }

            var userData = JsonSerializer.Deserialize<UserData>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            
            return userData ?? new UserData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения пользователей: {ex.Message}");
            return new UserData();
        }
    }
}