using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UpdateStatusFormData
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string StatusName { get; set; } = null!;
}