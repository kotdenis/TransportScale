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

        public async Task<PagingResponse<JournalDto>> GetPagedJournalAsync(JournalParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(AppConstants.JournalUrl + "paged", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<JournalDto>
            {
                Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JournalDto>>(content),
                MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<Metadata>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<JournalDto>> SearchWeighingAsync(SearchModel model, JournalParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var requestUri = QueryHelpers.AddQueryString(AppConstants.JournalUrl + "search", queryStringParam);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUri),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(model)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<JournalDto>
            {
                Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JournalDto>>(content),
                MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<Metadata>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

    }
}
