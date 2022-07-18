using TransportScale.Dto.DtoModels;

namespace TransportScale.Dto
{
    public class JournalState
    {
        public List<ForDayModel> JournalForDay { get; set; } = new List<ForDayModel>();
        public List<JournalDto> JournalTotal { get; set; } = new List<JournalDto>();
        public bool IsInSearchState { get; set; }
    }
}
