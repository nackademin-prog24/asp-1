using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ClientEntity
{
    [Key]
    public int Id { get; set; }
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Location { get; set; }

    [Column(TypeName = "date")]
    public DateTime Created { get; set; }
    
    public bool IsActive { get; set; }
}
