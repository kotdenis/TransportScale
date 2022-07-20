using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Interfacies
{
    public interface ITransportRepository : IGenericRepository<Transport>
    {
        Task<bool> SoftDeleteAsync(Transport transport, CancellationToken ct);
    }
}
