using Microsoft.EntityFrameworkCore;
using Million.Domain.Interfaces.Repositories.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Infra.Data.Repositories.Transversal
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            var myEntity = await _context.Set<T>().AddAsync(entity);           
            if (await _context.SaveChangesAsync() > -1)
            {
                return myEntity.Entity;
            }
            else
            {
                return entity;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var myEntity = _context.Set<T>().Update(entity);
            if (await _context.SaveChangesAsync() > -1)
            {
                return myEntity.Entity;
            }
            else
            {
                return entity;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                if (await _context.SaveChangesAsync() > -1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
