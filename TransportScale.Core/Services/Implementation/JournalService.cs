using AutoMapper;
using TransportScale.Core.Services.Interfacies;
using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Services.Implementation
{
    public class JournalService : IJournalService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IJournalRepository _journalRepository;
        private readonly IMapper _mapper;

        public JournalService(ICacheManager cacheManager,
            IJournalRepository journalRepository,
            IMapper mapper)
        {
            _cacheManager = cacheManager;
            _journalRepository = journalRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JournalDto>> GetJournalDtosAsync(CancellationToken ct)
        {
            var key = "journal";
            var result = await _cacheManager.GetAsync(key, async () =>
            {
                var journals = await _journalRepository.GetAllAsync(ct);
                journals = journals.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Created).ToList();
                var dtos = _mapper.ProjectTo<JournalDto>(journals.AsQueryable());
                var list = dtos.ToList();
                return list;
            });
            var journalDtos = await result;
            return journalDtos;
        }

        public async Task<IEnumerable<JournalDto>> SearchInJournalAsync(SearchModel searchModel, CancellationToken ct)
        {
            var journals = await _journalRepository.SearchByNumberandDateAsync(searchModel, ct);
            var dtos = _mapper.ProjectTo<JournalDto>(journals.AsQueryable());
            var list = dtos.ToList();
            return list;
        }
    }
}
