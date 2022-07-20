using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using TransportScale.Core.Services.Interfacies;
using TransportScale.Data.Repositries.Interfacies;
using TransportScale.Dto.DtoModels;
using TransportScale.Dto.Pagination;
using TransportScale.Entity.Entities;

namespace TransportScale.Core.Services.Implementation
{
    public class TransportService : ITransportService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ITransportRepository _transportRepository;
        private readonly IJournalRepository _journalRepository;
        private readonly IMapper _mapper;
        private readonly ITransportQuantityRepository _quantityRepository;
        private readonly IValidator<TransportDto> _validator;
        private readonly IValidator<JournalDto> _validatorJournal;
        private readonly IValidator<TransportModel> _validatorModel;

        public TransportService(ICacheManager cacheManager,
            ITransportRepository transportRepository,
            IJournalRepository journalRepository,
            ITransportQuantityRepository quantityRepository,
            IValidator<TransportDto> validator,
            IValidator<JournalDto> validatorJournal,
            IValidator<TransportModel> validatorModel,
            IMapper mapper)
        {
            _cacheManager = cacheManager;
            _transportRepository = transportRepository;
            _journalRepository = journalRepository;
            _quantityRepository = quantityRepository;
            _validator = validator;
            _validatorJournal = validatorJournal;
            _validatorModel = validatorModel;
            _mapper = mapper;
        }

        public async Task<TransportDto> GetRandomTransportAsync(CancellationToken ct)
        {
            var key = "transport";
            var result = await _cacheManager.GetAsync(key, async () =>
            {
                var entities = await _transportRepository.GetAllAsync(ct);
                entities = entities.ToList();
                await _cacheManager.SetAsync(key, entities);
                return entities;
            }).GetAwaiter().GetResult();
            var quantities = await _quantityRepository.GetAllAsync(ct);
            var quantity = quantities.Select(x => x.Quantity).FirstOrDefault();
            var random = new Random();
            var transportId = random.Next(quantity);
            var entity = result.Where(x => x.Id == transportId).FirstOrDefault();
            var dto = _mapper.Map<TransportDto>(entity);
            return dto;
        }

        public async Task<TransportDto> GetRandomTransportAsync2(CancellationToken ct)
        {
            var entities = await _transportRepository.GetAllAsync(ct);
            var quantities = await _quantityRepository.GetAllAsync(ct);
            var quantity = quantities.Select(x => x.Quantity).FirstOrDefault();
            var random = new Random();
            var transportId = random.Next(quantity);
            var entity = entities.Where(x => x.Id == transportId).FirstOrDefault();
            var dto = _mapper.Map<TransportDto>(entity);
            return dto;
        }

        public async Task SaveTransportWeightAsync(JournalDto journalDto, CancellationToken ct)
        {
            ValidationResult valid = await _validatorJournal.ValidateAsync(journalDto, ct);
            if (!valid.IsValid)
                throw new ValidationException("The model is incorrect");
            var journal = _mapper.Map<Journal>(journalDto);
            journal.WeighinDate = DateTime.UtcNow;
            journal.Date = DateTime.UtcNow.ToShortDateString();
            journal.Time = DateTime.UtcNow.ToShortTimeString();
            await _journalRepository.CreateAsync(journal, ct);
        }

        public async Task<List<ForDayModel>> GetWeighingForDayAsync(CancellationToken ct)
        {
            var journals = await _journalRepository.GetAllAsync(ct);
            var now = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
            var list = journals.Where(x => x.WeighinDate >= now).ToList(); 
            var models = new List<ForDayModel>();
            foreach (var journal in list)
            {
                models.Add(new ForDayModel
                {
                    CarPlate = journal.Number,
                    Time = journal.Time,
                    Weight = journal.Weight
                });
            }
            if (list.Count > 0)
            {
                return models;
            }
            else
                return new List<ForDayModel>();
        }

        public async Task<PagedList<ForDayModel>> GetWeighingForDayAsync2(JournalParameters parameters, CancellationToken ct)
        {
            var journals = await _journalRepository.GetAllAsync(ct);
            var now = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
            var forDayTransports = journals.Where(x => x.WeighinDate >= now).ToList();
            var list = new List<ForDayModel>();
            foreach(var journal in forDayTransports)
            {
                list.Add(new ForDayModel
                {
                    CarPlate = journal.Number,
                    Time = journal.Time,
                    Weight = journal.Weight
                });
            }
            return PagedList<ForDayModel>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<bool> CreateNewTransportAsync(TransportModel model, CancellationToken ct)
        {
            ValidationResult result = await _validatorModel.ValidateAsync(model, ct);

            if (result.IsValid)
            {
                //await _cacheManager.ClearlAsync("transport");
                var quantities = await _quantityRepository.GetAllAsync(ct);
                var quantity = quantities.Select(x => x).FirstOrDefault();
                quantity.Quantity = quantity.Quantity + 1;
                var transport = _mapper.Map<Transport>(model);
                await _transportRepository.CreateAsync(transport, ct);
                await _quantityRepository.UpdateAsync(quantity, ct);
                return true;
            }
            return false;
        }

        public async Task<List<TransportDto>> GetAllAsync(CancellationToken ct)
        {
            var transports = await _transportRepository.GetAllAsync(ct);
            var dtos = _mapper.ProjectTo<TransportDto>(transports.AsQueryable());
            return dtos.ToList();
        }

        public async Task<PagedList<TransportDto>> GetAllTransportsPagedAsync(JournalParameters parameters, CancellationToken ct)
        {
            var transports = await _transportRepository.GetAllAsync(ct);
            if (transports.Any())
            {
                var dtos = _mapper.ProjectTo<TransportDto>(transports.Where(x => x.IsDeleted == false).AsQueryable());
                var list = dtos.ToList();
                return PagedList<TransportDto>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);
            }
            return null;
        }

        public async Task<bool> DeleteTransportAsync(TransportDto transportDto, CancellationToken ct)
        {
            var transports = await _transportRepository.GetAllAsync(ct);
            var transport = transports.Where(x => x.Number == transportDto.Number).FirstOrDefault();
            bool isDeleted = false;
            if (transport != null)
                isDeleted = await _transportRepository.SoftDeleteAsync(transport, ct);
            return isDeleted;
        }

        public async Task UpdateAsync(TransportDto transportDto, CancellationToken ct)
        {
            var transports = await _transportRepository.GetAllAsync(ct);
            var temp = transports.Where(x => x.Number == transportDto.Number).FirstOrDefault();
            
            if(temp != null)
            {
                var transport = _mapper.Map<Transport>(transportDto);
                await _transportRepository.UpdateAsync(transport, ct);
            }
        }
    }
}
