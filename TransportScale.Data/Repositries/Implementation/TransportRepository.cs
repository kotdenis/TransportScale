using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Implementation
{
    public class TransportRepository : GenericRepository<Transport>, ITransportRepository
    {
        public TransportRepository(TransportDbContext transportDbContext) : base(transportDbContext)
        {

        }
    }
}
