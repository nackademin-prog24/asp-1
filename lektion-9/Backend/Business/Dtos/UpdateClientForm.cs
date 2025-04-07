using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UpdateClientForm
{
    [Required]
    public int Id { get; set; }

    public string? ImageFileName { get; set; }
    public IFormFile? NewImageFile { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;
}
