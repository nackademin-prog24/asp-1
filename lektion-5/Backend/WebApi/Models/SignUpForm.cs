using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class SignUpForm
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(2)]
    public string LastName { get; set; } = null!;

    [Required]
    [MinLength(5)]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$")]
    public string Password { get; set; } = null!;

    public string? Role { get; set; }
}
