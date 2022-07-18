using FluentValidation;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Validation
{
    public class TransportModelValidation : AbstractValidator<TransportModel>
    {
        public TransportModelValidation()
        {
            RuleFor(x => x.Weight)
                .GreaterThan(1);
            RuleFor(x => x.AxisNumber)
                .GreaterThan(1);
        }
    }
}
