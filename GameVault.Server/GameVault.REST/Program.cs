using GameVault.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GameVault.REST;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.Extensions.Azure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Get configurations.
IConfiguration configuration = builder.Configuration;

// Add services to the container.
IServiceCollection services = builder.Services;
services.AddControllers();

string SQLdbConnectionString = string.Empty;
string redisConnectionString = string.Empty;
if (builder.Environment.IsDevelopment())
{
    SQLdbConnectionString = configuration.GetConnectionString("Game Vault SQL")!;
    redisConnectionString = configuration.GetConnectionString("Redis Cache")!;
}
else if (builder.Environment.IsProduction())
{
    Uri keyVaultURI = new(configuration["KeyVault:URI"]!);
    string tenantId = configuration["KeyVault:DirectoryId"]!;
    string clientId = configuration["KeyVault:ClientId"]!;
    string clientSecret = configuration["KeyVault:ClientSecret"]!;
    ClientSecretCredential clientSecretCredential = new(tenantId, clientId, clientSecret);

    builder.Configuration.AddAzureKeyVault(keyVaultURI, clientSecretCredential);
    SecretClient secretClient = new(keyVaultURI, clientSecretCredential);
  
    SQLdbConnectionString = configuration["ProdConnection"]!;
    //redisConnectionString = (await secretClient.GetSecretAsync("RedisConnection")).Value.Value;
}

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(SQLdbConnectionString, actions => actions.MigrationsAssembly("GameVault.REST")));
services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);

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
