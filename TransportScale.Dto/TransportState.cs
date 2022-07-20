using TransportScale.Dto.DtoModels;

namespace TransportScale.Dto
{
    public class TransportState
    {
        public List<TransportDto> TransportDtos { get; set; } = new List<TransportDto>();
        public List<TransportDto> TotalTransportDtos { get; set; } = new List<TransportDto>();
    }
}
