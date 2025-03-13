namespace Business.Models;

public class Client
{
    public int Id { get; set; }
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
}
