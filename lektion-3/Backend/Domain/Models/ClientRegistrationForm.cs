﻿using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class ClientRegistrationForm
{
    public IFormFile? Image { get; set; }
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Reference { get; set; }
    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
