using TransportScale.Dto.DtoModels;
using TransportScale.Entity.Entities;

namespace TransportScale.Data.Repositries.Interfacies
{
    public interface IJournalRepository : IGenericRepository<Journal>
    {
        Task<IEnumerable<Journal>> SearchByNumberandDateAsync(SearchModel searchModel, CancellationToken ct);
    }
}
