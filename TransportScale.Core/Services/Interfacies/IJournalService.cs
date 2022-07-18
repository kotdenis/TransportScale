using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;

namespace TransportScale.Core.Services.Interfacies
{
    public interface IJournalService
    {
        Task<IEnumerable<JournalDto>> GetJournalDtosAsync(CancellationToken ct);
        Task<PagedList<JournalDto>> SearchInJournalAsync(SearchModel searchModel, JournalParameters parameters, CancellationToken ct);
        Task<PagedList<JournalDto>> GetPagedJournalDtosAsync(JournalParameters parameters, CancellationToken ct);
    }
}
