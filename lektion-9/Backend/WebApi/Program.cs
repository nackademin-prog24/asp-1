using Business.Handlers;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();



builder.Services.AddAuthorization();

builder.Services.AddScoped(typeof(ICacheHandler<>), typeof(CacheHandler<>));
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProjectRepository>();

builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Alpha BackOffice API");
    options.RoutePrefix = string.Empty;
});


app.MapControllers();
app.MapOpenApi();

app.Run();