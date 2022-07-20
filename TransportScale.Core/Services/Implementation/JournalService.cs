using AutoMapper;
using TransportScale.Core.Services.Interfacies;
using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;
using TransportScale.Entity.Entities;

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
            var journals = await _journalRepository.GetAllAsync(ct);
            journals = journals.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Created).ToList();
            var dtos = _mapper.ProjectTo<JournalDto>(journals.AsQueryable());
            var list = dtos.ToList();
            return list;
        } 

        public async Task<PagedList<JournalDto>> GetPagedJournalDtosAsync(JournalParameters parameters, CancellationToken ct)
        {
            var journals = await _journalRepository.GetAllAsync(ct);
            journals = journals.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Created).OrderByDescending(x => x.WeighinDate).ToList();
            var dtos = _mapper.ProjectTo<JournalDto>(journals.AsQueryable());
            var list = dtos.ToList();
            return PagedList<JournalDto>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<JournalDto>> SearchInJournalAsync(SearchModel searchModel, JournalParameters parameters, CancellationToken ct)
        {
            var journals = await _journalRepository.GetAllAsync(ct);
            var searchedJournals = new List<Journal>();
            if(searchModel.DateOfWeighing > DateTime.MinValue && !string.IsNullOrEmpty(searchModel.CarPlate))
                searchedJournals = journals.Where(x => x.Number == searchModel.CarPlate && x.WeighinDate.Date == searchModel.DateOfWeighing.Value.Date && 
                    x.IsDeleted == false).ToList();
            else if (searchModel.DateOfWeighing > DateTime.MinValue && string.IsNullOrEmpty(searchModel.CarPlate))
                searchedJournals = journals.Where(x => x.WeighinDate.Date == searchModel.DateOfWeighing.Value.Date &&
                    x.IsDeleted == false).ToList();
            else if (searchModel.DateOfWeighing == null && !string.IsNullOrEmpty(searchModel.CarPlate))
                searchedJournals = journals.Where(x => x.Number == searchModel.CarPlate  && x.IsDeleted == false).ToList();

            var dtos = _mapper.ProjectTo<JournalDto>(searchedJournals.AsQueryable());
            var list = dtos.ToList();
            return PagedList<JournalDto>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);
        }
    }
}
