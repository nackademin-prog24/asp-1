using Microsoft.AspNetCore.Http;

namespace Business.Dtos;

public class SignUpDto
{
    public IFormFile? FormFile { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "User";
}
