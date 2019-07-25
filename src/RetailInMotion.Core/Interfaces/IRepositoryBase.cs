using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetailInMotion.Core.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetByIdAsync(Guid? id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
