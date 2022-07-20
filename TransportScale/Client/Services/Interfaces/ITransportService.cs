using TransportScale.Client.Features;
using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;

namespace TransportScale.Client.Services.Interfaces
{
    public interface ITransportService
    {
        Task<TransportDto> GetRandomTransporAsync();
        Task<bool> SaveWeightAsync(JournalDto journal);
        Task<List<ForDayModel>> GetForDayModelsAsync();
        Task<PagingResponse<ForDayModel>> GetPagedForDayAsync(JournalParameters parameters);
        Task<bool> CreateNewTransportAsync(TransportModel transportModel);
        Task<PagingResponse<TransportDto>> GetAllAsync(JournalParameters parameters);
        Task<bool> DeleteAsync(TransportDto transportDto);
        Task<List<TransportDto>> GetAllTransportsAsync();
        Task UpdateAsync(TransportDto transportDto);
    }
}
