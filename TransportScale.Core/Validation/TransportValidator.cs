using FluentValidation;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Validation
{
    public class TransportValidator : AbstractValidator<TransportDto>
    {
        public TransportValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Cargo).NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Weight).NotEmpty();
            RuleFor(x => x.AxisNumber).NotEmpty()
                .InclusiveBetween(2, 10);
            RuleFor(x => x.Number).NotEmpty()
                .MinimumLength(5);

        }
    }
}
