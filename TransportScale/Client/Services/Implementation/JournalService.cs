using System.Net.Http.Json;
using TransportScale.Client.Constants;
using TransportScale.Client.Services.Interfaces;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Client.Services.Implementation
{
    public class JournalService : IJournalService
    {
        private readonly HttpClient _httpClient;

        public JournalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JournalDto>> GetJournalDtosAsync()
        {
            var journals = await _httpClient.GetFromJsonAsync<List<JournalDto>>(new Uri(AppConstants.JournalUrl + "journal"));
            if (journals == null)
                journals = new List<JournalDto>();
            return journals;
        }
    }
}
