﻿namespace Business.Models;

public class Client
{
    public int Id { get; set; }
    public string? ImageFileName { get; set; }
    public string ClientName { get; set; } = null!;
}
