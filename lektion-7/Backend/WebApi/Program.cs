using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();


var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
