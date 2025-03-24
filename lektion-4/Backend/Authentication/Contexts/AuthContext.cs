﻿using Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Contexts;

public class AuthContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<AppUserProfile> UserProfiles { get; set; }
    public DbSet<AppUserAddress> UserAddresses { get; set; }
}
