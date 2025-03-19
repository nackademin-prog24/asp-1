using Business.Services;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => 
    x.UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("AzureDB")));

builder.Services.AddScoped<ProjectStatusRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<ProjectStatusService>();
builder.Services.AddScoped<ClientService>();

builder.Services.AddCors(x =>
{
    x.AddPolicy("Strict", x =>
    {
        x.WithOrigins("http://localhost:5173")
         .WithMethods("GET", "POST", "PUT", "DELETE")
         .WithHeaders("Content-Type", "Authorization")
         .AllowCredentials();
    });

    x.AddPolicy("AllowAll", x =>
    {
        x.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();

    });
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAll");
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Alpha API");
    x.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
