using Microsoft.EntityFrameworkCore;
using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly TransportDbContext _transportDbContext;

        public GenericRepository(TransportDbContext transportDbContext)
        {
            _transportDbContext = transportDbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _transportDbContext.Set<TEntity>().ToListAsync(ct);
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken ct)
        {
            var entity = _transportDbContext.Set<TEntity>().Find(id);
            await Task.CompletedTask;
            return entity;
        }

        public async Task<IEnumerable<TEntity>> HardDeleteAsync(int id, CancellationToken ct)
        {
            var entity = _transportDbContext.Set<TEntity>().Find(id);
            _transportDbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            await _transportDbContext.SaveChangesAsync(ct);
            return await _transportDbContext.Set<TEntity>().ToListAsync(ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            //_transportDbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            _transportDbContext.ChangeTracker.Clear();
            _transportDbContext.Update(entity);
            await _transportDbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(TEntity entity, CancellationToken ct)
        {
            _transportDbContext.Entry<TEntity>(entity).State = EntityState.Added;
            await _transportDbContext.SaveChangesAsync(ct);
            return await _transportDbContext.Set<TEntity>().ToListAsync(ct);
        }
    }
}
