using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Repositories.Transversal
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
