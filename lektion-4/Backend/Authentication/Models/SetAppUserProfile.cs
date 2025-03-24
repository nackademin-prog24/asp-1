using System.ComponentModel.DataAnnotations;

namespace Authentication.Models;

public class SetAppUserProfile
{

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }


    public static implicit operator SetAppUserProfile(SignUp signUp)
    {
        return new SetAppUserProfile
        {
            FirstName = signUp.FirstName,
            LastName = signUp.LastName,
            PhoneNumber = signUp.PhoneNumber
        };
    }
}
