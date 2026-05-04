namespace BlazorApp.ApiRequest.Model;

public class UserDataShort
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
    public string RoleName { get; set; } 
}

public class UserData
{
    public UserDataContainer data  { get; set; }
    public bool status { get; set; }
}

public class UserDataContainer
{
    public List<UserDataShort> users { get; set; }
}

public class UserAddData
{
    public Guid UserID { get; set; }
    public string UserName { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public string RoleName { get; set; } 
    public string Action { get; set; }
    public string AvatarUrl { get; set; }
    public bool Status { get; set; }
}

public class UserUpdateData
{
    public bool status { get; set; }
}

public class UserUpdateAva
{
    public bool status { get; set; }
    public string AvatarUrl { get; set; }
}