using HotelBookingPlatform.Core.Entities;
using HotelBookingPlatform.Core.Repositories.Contract;
using HotelBookingPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
            => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _dbContext.Set<T>().Remove(entity);

        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);
    }
}
