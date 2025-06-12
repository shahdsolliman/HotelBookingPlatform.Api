using HotelBookingPlatform.Core;
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
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new GenericRepository<TEntity>();
                _repositories[type] = repoInstance;
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
        public Task<int> CompleteAsync()
            => Task.FromResult(1);

        public ValueTask DisposeAsync()
            => ValueTask.CompletedTask;

    }
}
