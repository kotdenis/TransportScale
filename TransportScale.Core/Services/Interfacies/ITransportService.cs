using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;

namespace TransportScale.Core.Services.Interfacies
{
    public interface ITransportService
    {
        Task<TransportDto> GetRandomTransportAsync(CancellationToken ct);
        Task SaveTransportWeightAsync(JournalDto journalDto, CancellationToken ct);
        Task<bool> CreateNewTransportAsync(TransportModel model, CancellationToken ct);
        Task<List<ForDayModel>> GetWeighingForDayAsync(CancellationToken ct);
        Task<PagedList<ForDayModel>> GetWeighingForDayAsync2(JournalParameters parameters, CancellationToken ct);
        Task<TransportDto> GetRandomTransportAsync2(CancellationToken ct);
        Task<PagedList<TransportDto>> GetAllTransportsPagedAsync(JournalParameters parameters, CancellationToken ct);
        Task<bool> DeleteTransportAsync(TransportDto transportDto, CancellationToken ct);
        Task<List<TransportDto>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(TransportDto transportDto, CancellationToken ct);
    }
}
