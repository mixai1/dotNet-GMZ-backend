using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotNet_GMZ_backend.Models.Models;

namespace dotNet_GMZ_backend.Core
{
    public interface IRepository<T> where T : Entity
    {
        Task SaveAsync();
        Task<List<T>> GetAllAsync();
        Task<bool> CreateAsync(T obj);
        Task<List<T>> FindAsync(Expression<Func<T,bool>> predicate);
        Task<T> FindById(Guid id);
        Task<bool> RemoveById(Guid id);
    }
}