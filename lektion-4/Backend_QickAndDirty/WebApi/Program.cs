using Authentication.Contexts;
using Authentication.Models;
using Authentication.Services;
using Business.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AuthContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AuthContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<ITokenManager, TokenManager>();



var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
