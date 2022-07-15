using TransportScale.Dto.DtoModels;

namespace TransportScale.Client.Services.Interfaces
{
    public interface ITransportService
    {
        Task<TransportDto> GetRandomTransporAsync();
        Task<List<JournalDto>> SaveWeightAsync(JournalDto journal);
    }
}
