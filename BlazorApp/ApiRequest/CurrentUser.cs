using BlazorApp.ApiRequest.Model;

namespace BlazorApp.ApiRequest;

public class CurrentUser
{
    public int CurrentID;
    public string CurrentName;
    public string CurrentMail;
    public string CurrentPassword;
    public string CurrentRole;
    public bool IsLoggedIn = false;
    
    public void Login(UserAddData user)
    {
        CurrentID = user.UserID;
        CurrentName = user.UserName;
        CurrentMail = user.Mail;
        CurrentPassword = user.Password;
        CurrentRole = user.Role;
        IsLoggedIn = true;
        Console.WriteLine(CurrentRole);
        Console.WriteLine(IsLoggedIn);
    }
    
    public void Logout()
    {
        CurrentRole = "";
        IsLoggedIn = false;
    }
}