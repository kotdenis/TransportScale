using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportScale.Core.Services.Interfacies;
using TransportScale.Dto.DtoModels;

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
            var randomTransport = await _transportService.GetRandomTransportAsync(ct);
            return Ok(randomTransport);
        }

        [HttpPost("save")]
        public async Task<ActionResult<List<JournalDto>>> SaveTransportWeigh([FromBody] JournalDto journal, CancellationToken ct = default)
        {
            if (journal == null)
                return BadRequest();
            var journals = await _transportService.SaveTransportWeightAsync(journal, ct);
            return Ok(journals);
        }

        [HttpPost("new-transport")]
        public async Task<ActionResult<bool>> CreateNewTransport([FromBody] TransportDto transport, CancellationToken ct = default)
        {
            if (transport == null)
                return BadRequest(false);
            await _transportService.CreateNewTransportAsync(transport, ct);
            return Ok(true);
        }
    }
}
