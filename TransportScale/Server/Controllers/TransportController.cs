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
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpGet("random")]
        public async Task<ActionResult<TransportDto>> GetRandomTransport(CancellationToken ct = default)
        {
            var randomTransport = await _transportService.GetRandomTransportAsync2(ct);
            return Ok(randomTransport);
        }

        [HttpPost("save")]
        public async Task<ActionResult<List<JournalDto>>> SaveTransportWeigh([FromBody] JournalDto journal, CancellationToken ct = default)
        {
            if (journal == null)
                return BadRequest();
            await _transportService.SaveTransportWeightAsync(journal, ct);
            return Ok();
        }

        [HttpGet("forday")]
        public async Task<ActionResult<List<ForDayModel>>> GetForDayModel(CancellationToken ct = default)
        {
            var models = await _transportService.GetWeighingForDayAsync(ct);
            return Ok(models);
        }

        [HttpPost("new")]
        public async Task<ActionResult<bool>> CreateNewTransport([FromBody] TransportModel model, CancellationToken ct = default)
        {
            var isCreated = await _transportService.CreateNewTransportAsync(model, ct);
            if (isCreated)
                return Ok(true);
            else
                return BadRequest(false);
        }

        [HttpGet("forday2")]
        public async Task<ActionResult<PagedList<ForDayModel>>> GetPagedForDay([FromQuery] JournalParameters parameters, CancellationToken ct = default)
        {
            parameters.PageSize = 9;
            var models = await _transportService.GetWeighingForDayAsync2(parameters, ct);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(models.MetaData));
            return Ok(models);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PagedList<TransportDto>>> GetAll([FromQuery] JournalParameters parameters, CancellationToken ct = default)
        {
            parameters.PageSize = 8;
            var transports = await _transportService.GetAllTransportsPagedAsync(parameters, ct);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(transports.MetaData));
            return Ok(transports);
        }

        [HttpDelete("soft")]
        public async Task<ActionResult<bool>> Delete([FromBody] TransportDto transportDto, CancellationToken ct = default)
        {
            var isDeleted = await _transportService.DeleteTransportAsync(transportDto, ct);
            if (isDeleted)
                return Ok(true);
            else
                return BadRequest(false);
        }

        [HttpGet]
        public async Task<ActionResult<List<TransportDto>>> GetTransportsTotal(CancellationToken ct = default)
        {
            var result = await _transportService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TransportDto transportDto, CancellationToken ct = default)
        {
            await _transportService.UpdateAsync(transportDto, ct);
            return Ok();
        }
    }
}
