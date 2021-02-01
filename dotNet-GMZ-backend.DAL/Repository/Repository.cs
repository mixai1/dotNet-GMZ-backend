using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(AppDbContext appDbContext, ILogger<Repository<T>> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var result = await _appDbContext.Set<T>().Take(10).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(GetAllAsync), e);
                return new List<T>();
            }
        }

        public async Task<bool> CreateAsync(T obj)
        {
            try
            {
                var result = await _appDbContext.Set<T>().AddAsync(obj);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(CreateAsync), e);
                return false;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var result = await _appDbContext.Set<T>().Where(predicate).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(FindAsync), e);
                return new List<T>();
            }
        }

        public async Task<T> FindById(Guid id)
        {
            try
            {
                var result = await _appDbContext.Set<T>().FindAsync(id);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(FindById), e);
                return null;
            }
        }

        public async Task<bool> RemoveById(Guid id)
        {
            try
            {
                var result = await _appDbContext.Set<T>().FindAsync(id);
                if (result is null)
                {
                    return false;
                }

                _appDbContext.Set<T>().Remove(result);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RemoveById), e);
                return false;
            }
        }
    }
}