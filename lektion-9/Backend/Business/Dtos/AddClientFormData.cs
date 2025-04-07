using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class AddClientFormData
{
    public IFormFile? ImageFile { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;
}
