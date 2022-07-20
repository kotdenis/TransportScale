using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Interfacies
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct);
        Task<TEntity> GetByIdAsync(int id, CancellationToken ct);
        //Task<bool> SoftDeleteAsync<T>(int id, CancellationToken ct);
        Task<IEnumerable<TEntity>> HardDeleteAsync(int id, CancellationToken ct);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);
        Task<IEnumerable<TEntity>> CreateAsync(TEntity entity, CancellationToken ct);

    }
}
