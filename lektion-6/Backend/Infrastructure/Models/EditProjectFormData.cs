﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class EditProjectFormData
{
    [Required]
    public string Id { get; set; } = null!;
    public string? Image { get; set; }

    [Required]
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }

    [Required]
    public string ClientId { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public int StatusId { get; set; }
}
