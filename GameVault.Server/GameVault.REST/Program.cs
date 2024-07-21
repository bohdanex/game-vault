using GameVault.Repository;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Get configurations.
IConfiguration configuration = builder.Configuration;

// Add services to the container.
IServiceCollection services = builder.Services;
services.AddControllers();
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("GameVault"), actions => actions.MigrationsAssembly("GameVault.REST")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
