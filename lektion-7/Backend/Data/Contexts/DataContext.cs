﻿using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<UserAddressEntity> UserAddresses { get; set; }
    public DbSet<PostalCodeEntity> PostalCodes { get; set; }
}
