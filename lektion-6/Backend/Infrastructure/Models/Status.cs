﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class Status
{
    public int Id { get; set; }
    public string StatusName { get; set; } = null!;
}
