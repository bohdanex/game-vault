using GameVault.ObjectModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Repository.Abstraction
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        protected RepositoryBase(AppDbContext context)
        {
            _dbContext = context;
        }

        public virtual async Task<TEntity?> GetById(Guid id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbContext.Remove<TEntity>(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        { 
            _dbContext.Update<TEntity>(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
