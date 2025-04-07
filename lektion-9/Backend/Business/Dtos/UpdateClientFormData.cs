using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UpdateClientFormData
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;
}
