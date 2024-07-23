using GameVault.ObjectModel.Entities;

namespace GameVault.Repository.Abstraction
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task Register(User user);
        Task<User?> GetByNameOrEmail(string nickname, string email);
    }
}
