using FluentValidation;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Validation
{
    public class JournalValidator : AbstractValidator<JournalDto>
    {
        public JournalValidator()
        {
            RuleFor(x => x.Weight).NotEmpty()
                .GreaterThan(1000);
            RuleFor(x => x.Name).NotEmpty()
                .NotNull();
            RuleFor(x => x.Number).NotEmpty()
                .NotNull();
            RuleFor(x => x.Cargo).NotEmpty()
                .NotNull();
        }
    }
}
