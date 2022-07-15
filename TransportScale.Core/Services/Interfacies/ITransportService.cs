using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Services.Interfacies
{
    public interface ITransportService
    {
        Task<TransportDto> GetRandomTransportAsync(CancellationToken ct);
        Task<List<JournalDto>> SaveTransportWeightAsync(JournalDto journalDto, CancellationToken ct);
        Task CreateNewTransportAsync(TransportDto transportDto, CancellationToken ct);
    }
}
