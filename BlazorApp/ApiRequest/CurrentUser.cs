using BlazorApp.ApiRequest.Model;

namespace BlazorApp.ApiRequest;

public class CurrentUser
{
    public Guid CurrentID;
    public string CurrentName;
    public string CurrentMail;
    public string CurrentPassword;
    public string CurrentAvatar;
    public string CurrentRole;
    public bool IsLoggedIn = false;
    
    public void Login(UserAddData user)
    {
        CurrentID = user.UserID;
        CurrentName = user.UserName;
        CurrentMail = user.Mail;
        CurrentPassword = user.Password;
        CurrentAvatar = user.AvatarUrl;
        CurrentRole = user.RoleName;
        IsLoggedIn = true;
    }
}