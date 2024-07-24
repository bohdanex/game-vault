using GameVault.Repository;
using GameVault.Services;
using GameVault.Services.Abstraction;
using GameVault.Repository.Abstraction;
using GameVault.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using GameVault.REST;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Get configurations.
IConfiguration configuration = builder.Configuration;

// Add services to the container.
IServiceCollection services = builder.Services;
services.AddControllers();

string dbConnectionString = builder.Environment.IsDevelopment() ? configuration.GetConnectionString("Game Vault SQL")! : "";
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString, actions => actions.MigrationsAssembly("GameVault.REST")));
services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("Redis Cache"));

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(conf =>
    {
        conf.TokenValidationParameters = new()
        {
            ValidIssuer = configuration["JWT_Settings:Issuer"],
            ValidAudience = configuration["JWT_Settings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_Settings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true
        };
    });
services.AddAuthorization();

services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
