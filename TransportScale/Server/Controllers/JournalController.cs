using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TransportScale.Core.Services.Interfacies;
using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;

namespace TransportScale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpGet("journal")]
        public async Task<ActionResult<List<JournalDto>>> GetJournalDtos(CancellationToken ct = default)
        {
            var journalDto = await _journalService.GetJournalDtosAsync(ct);
            return Ok(journalDto);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PagedList<JournalDto>>> SearchInJournal([FromBody] SearchModel searchModel, [FromQuery] JournalParameters parameters, CancellationToken ct = default)
        {
            parameters.PageSize = 10;
            var searchDtos = await _journalService.SearchInJournalAsync(searchModel, parameters, ct);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(searchDtos.MetaData));
            return Ok(searchDtos);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<JournalDto>>> GetPagedJournalDtos([FromQuery] JournalParameters parameters, CancellationToken ct = default)
        {
            parameters.PageSize = 10;
            var models = await _journalService.GetPagedJournalDtosAsync(parameters, ct);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(models.MetaData));
            return Ok(models);
        }
    }
}
