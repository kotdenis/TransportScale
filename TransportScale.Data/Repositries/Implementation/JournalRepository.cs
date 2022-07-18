using Microsoft.EntityFrameworkCore;
using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Dto.DtoModels;
using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Implementation
{
    public class JournalRepository : GenericRepository<Journal>, IJournalRepository
    {
        private readonly TransportDbContext _context;
        public JournalRepository(TransportDbContext transportDbContext) : base(transportDbContext)
        {
            _context = transportDbContext;
        }
    }
}
