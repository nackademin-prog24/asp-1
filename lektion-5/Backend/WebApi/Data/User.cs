using Microsoft.AspNetCore.Identity;

namespace WebApi.Data;

public class User : IdentityUser
{
    [ProtectedPersonalData]
    public string? FirstName { get; set; }

    [ProtectedPersonalData]
    public string? LastName { get; set; }
}
