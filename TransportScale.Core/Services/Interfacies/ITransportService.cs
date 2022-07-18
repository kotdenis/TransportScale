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
    }
}
