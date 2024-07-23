using GameVault.ObjectModel.Entities;

namespace GameVault.Repository.Abstraction
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task Add(TEntity entity);
        Task Remove(TEntity entity);
    }
}
