using System.ComponentModel;

namespace BlazorApp.ApiRequest.Model;

public class ImageDataShort
{
    public string ImageUrl { get; set; }
    public string ExcelUrl { get; set; }
}

public class ImageData
{ 
    public ImageDataContainer data { get; set; }
    public bool status { get; set; }
}

public class ImageAddData
{
    public bool status { get; set; }
}

public class ImageDataContainer
{
    public List<ImageDataShort> image { get; set; }
}