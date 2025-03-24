using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models;

public class AppUserProfile
{
    [PersonalData]
    [Key, ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    [ProtectedPersonalData]
    public string? FirstName { get; set; }

    [ProtectedPersonalData]
    public string? LastName { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

    [ProtectedPersonalData]
    public string? PhoneNumber { get; set; }

    public AppUser User { get; set; } = null!;
}
