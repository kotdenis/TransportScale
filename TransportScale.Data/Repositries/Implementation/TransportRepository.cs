using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Implementation
{
    public class TransportRepository : GenericRepository<Transport>, ITransportRepository
    {
        private readonly TransportDbContext _context;
        public TransportRepository(TransportDbContext transportDbContext) : base(transportDbContext)
        {
            _context = transportDbContext;
        }

        public async Task<bool> SoftDeleteAsync(Transport transport, CancellationToken ct)
        {
            _context.Entry(transport).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
