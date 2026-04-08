using System.ComponentModel;

namespace BlazorApp.ApiRequest.Model;

public class ImageDataShort
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}

public class ImageData
{ 
    public ImageDataContainer data { get; set; }
    public bool status { get; set; }
}

public class ImageDataContainer
{
    public List<ImageDataShort> image { get; set; }
}