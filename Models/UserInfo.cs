using UserRegistrationApi.Models;

public class UserInfo
{
    public int Id { get; set; }
    public string UserId { get; set; } = "";
    public ApplicationUser? User { get; set; }
    public string FirstName { get; set; } = "";
}