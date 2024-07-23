using GameVault.Repository.Abstraction;
using GameVault.Repository.Implementation;
using GameVault.Services.Abstraction;
using GameVault.Services;

namespace GameVault.REST
{
    internal static class ServiceInjector
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services) 
        {
            services.AddTransient<IPasswordHasher, PBKDF2PasswordHasher>();
            services.AddTransient<ITokenGenerator, JWT_TokenGenerator>();

            // Providers
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISteamGamesRepository, SteamGamesProvider>();
            services.AddTransient<IGameKeyRepository, GameKeyRepository>();
            //Servicesa

            services.AddTransient<IUserService, UserService>();
            services.AddHttpClient<ISteamGamesService, SteamGamesService>();
            services.AddTransient<IGameKeyService, GameKeyService>();

            return services;
        }
    }
}
