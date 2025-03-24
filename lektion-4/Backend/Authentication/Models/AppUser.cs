using Microsoft.AspNetCore.Identity;

namespace Authentication.Models;

public class AppUser : IdentityUser
{
    public AppUserProfile? Profile { get; set; }
    public AppUserAddress? Address { get; set; }
}
