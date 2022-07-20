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
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pagingResponse = new PagingResponse<ForDayModel>
                {
                    Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ForDayModel>>(content),
                    MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<Metadata>(response.Headers.GetValues("X-Pagination").First())
                };
                return pagingResponse;
            }
            return new PagingResponse<ForDayModel>();
        }

        public async Task<bool> CreateNewTransportAsync(TransportModel transportModel)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(AppConstants.TransportUrl + "new"),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(transportModel)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
            if(response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task UpdateAsync(TransportDto transportDto)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(AppConstants.TransportUrl),
                Method = HttpMethod.Put,
                Content = JsonContent.Create(transportDto)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
        }

        public async Task<PagingResponse<TransportDto>> GetAllAsync(JournalParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(AppConstants.TransportUrl + "all", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<TransportDto>
            {
                Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TransportDto>>(content),
                MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<Metadata>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<List<TransportDto>> GetAllTransportsAsync()
        {
            var transports = await _httpClient.GetFromJsonAsync<List<TransportDto>>(new Uri(AppConstants.TransportUrl));
            return transports;
        }

        public async Task<bool> DeleteAsync(TransportDto transportDto)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(AppConstants.TransportUrl + "soft"),
                Method = HttpMethod.Delete,
                Content = JsonContent.Create(transportDto)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
