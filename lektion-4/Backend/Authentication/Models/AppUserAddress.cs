using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models;

public class AppUserAddress
{
    [PersonalData]
    [Key, ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    [PersonalData]
    public string? StreetName { get; set; }

    [PersonalData]
    public string? PostalCode { get; set; }

    [PersonalData]
    public string? City { get; set; }

    public AppUser User { get; set; } = null!;
}
