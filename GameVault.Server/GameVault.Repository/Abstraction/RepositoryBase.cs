using GameVault.ObjectModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Repository.Abstraction
{
    public abstract class RepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Remove<T>(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity) 
        { 
            _context.Update<T>(entity);
            await _context.SaveChangesAsync();
        }
    }
}
