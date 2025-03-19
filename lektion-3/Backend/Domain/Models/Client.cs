namespace Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string? ClientName { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }
    public bool? IsActive { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Reference { get; set; }
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}
