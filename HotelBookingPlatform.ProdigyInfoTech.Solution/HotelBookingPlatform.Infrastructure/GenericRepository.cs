using HotelBookingPlatform.Core.Entities;
using HotelBookingPlatform.Core.Repositories.Contract;
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
        // In-memory data store using thread-safe dictionary
        private static readonly ConcurrentDictionary<Guid, T> _store = new();

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<T?> GetByIdAsync(Guid id)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid(); // Ensure the entity has a unique Id
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }
        public Task<bool> UpdateAsync(T entity)
        {
            if (!_store.ContainsKey(entity.Id))
                return Task.FromResult(false);

            _store[entity.Id] = entity;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

    }
}
