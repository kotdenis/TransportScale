using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Implementation
{
    public class TransportQuantityRepository : GenericRepository<TransportQuantity>, ITransportQuantityRepository
    {

        public TransportQuantityRepository(TransportDbContext transportDbContext) : base(transportDbContext)
        {

        }
    }
}
