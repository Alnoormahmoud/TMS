namespace TMS.Blazor.Components.Pages.User;

public class UserCardModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AccentClass { get; set; } = "border-primary";
    public string AvatarClass { get; set; } = "bg-blue-50 text-primary";

    public string SecondaryText => $"{Email} • {Role}";
}
