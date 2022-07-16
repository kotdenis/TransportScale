using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TransportScale.Client.Constants;
using TransportScale.Client.Features;
using TransportScale.Client.Services.Interfaces;
using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;

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

        public async Task<bool> SaveWeightAsync(JournalDto journal)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(AppConstants.TransportUrl + "save"),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(journal)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<ForDayModel>> GetForDayModelsAsync()
        {
            var models = await _httpClient.GetFromJsonAsync<List<ForDayModel>>(new Uri(AppConstants.TransportUrl + "forday"));
            return models;
        }

        public async Task<PagingResponse<ForDayModel>> GetPagedForDayAsync(JournalParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(AppConstants.TransportUrl + "forday2", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<ForDayModel>
            {
                Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ForDayModel>>(content),
                MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<Metadata>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }
    }
}
