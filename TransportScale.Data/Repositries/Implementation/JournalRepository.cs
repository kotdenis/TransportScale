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

        public async Task<IEnumerable<Journal>> SearchByNumberandDateAsync(SearchModel searchModel, CancellationToken ct)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            List<Journal> journals = new List<Journal>();
            if (DateTime.TryParse(searchModel.DateOfWeighing, out DateTime date) && !string.IsNullOrEmpty(searchModel.CarPlate))
            {
                journals = await _context.Set<Journal>().Where(x => x.Date == date.ToShortDateString() && x.Number == searchModel.CarPlate && x.IsDeleted == false).ToListAsync(ct);
            }
            else if (DateTime.TryParse(searchModel.DateOfWeighing, out DateTime date2) && string.IsNullOrEmpty(searchModel.CarPlate))
            {
                journals = await _context.Set<Journal>().Where(x => x.Date == date.ToShortDateString() && x.IsDeleted == false).ToListAsync(ct);
            }
            else if (!string.IsNullOrEmpty(searchModel.CarPlate))
            {
                journals = await _context.Set<Journal>().Where(x => x.Number == searchModel.CarPlate && x.IsDeleted == false).ToListAsync(ct);
            }
            return journals;
        }
    }
}
