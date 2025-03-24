using Microsoft.AspNetCore.Identity;

namespace Authentication.Models;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }

    public AppUserAddress? Address { get; set; }
}
