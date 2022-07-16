using TransportScale.Dto.DtoModels;

namespace TransportScale.Client.Services.Interfaces
{
    public interface IJournalService
    {
        Task<List<JournalDto>> GetJournalDtosAsync();
    }
}
