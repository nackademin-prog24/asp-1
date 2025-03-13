namespace Business.Models;

public class ClientRegistrationForm
{
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Location { get; set; }
}