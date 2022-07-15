using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TransportScale.Client.Constants;
using TransportScale.Client.Services.Interfaces;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Client.Services.Implementation
{
    public class TransportService : ITransportService
    {
        private readonly HttpClient _httpClient;

        public TransportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TransportDto> GetRandomTransporAsync()
        {
            var transport = await _httpClient.GetFromJsonAsync<TransportDto>(new Uri(AppConstants.TransportUrl + "random"));
            if (transport == null)
                throw new ArgumentNullException("There is no transport to accept.");
            return transport;
        }

        public async Task<List<JournalDto>> SaveWeightAsync(JournalDto journal)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(AppConstants.TransportUrl + "save"),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(journal)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<List<JournalDto>>(result);
            return list;
        }
    }
}
