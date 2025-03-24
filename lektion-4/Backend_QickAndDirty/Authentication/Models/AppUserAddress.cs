using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models;

public class AppUserAddress
{
    [Key, ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public string? StreetName { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }

    public AppUser User { get; set; } = null!;
}
