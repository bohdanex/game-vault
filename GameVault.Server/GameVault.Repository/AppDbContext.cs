using Microsoft.EntityFrameworkCore;
using GameVault.ObjectModel.Entities;

namespace GameVault.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; private set; }
        public DbSet<SteamGame> SteamGames { get; private set; }
        public DbSet<SteamGameKey> SteamGameKeys { get; private set; }
    }
}
