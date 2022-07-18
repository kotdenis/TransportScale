using TransportScale.Client.Features;
using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;

namespace TransportScale.Client.Services.Interfaces
{
    public interface IJournalService
    {
        Task<List<JournalDto>> GetJournalDtosAsync();
        Task<PagingResponse<JournalDto>> GetPagedJournalAsync(JournalParameters parameters);
        Task<PagingResponse<JournalDto>> SearchWeighingAsync(SearchModel model, JournalParameters parameters);
    }
}
