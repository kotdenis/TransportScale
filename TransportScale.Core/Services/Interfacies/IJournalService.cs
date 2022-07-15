using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Services.Interfacies
{
    public interface IJournalService
    {
        Task<IEnumerable<JournalDto>> GetJournalDtosAsync(CancellationToken ct);
        Task<IEnumerable<JournalDto>> SearchInJournalAsync(SearchModel searchModel, CancellationToken ct);
    }
}
