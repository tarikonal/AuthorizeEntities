
using Business.Handlers.Birims.Commands;
using FluentValidation;

namespace Business.Handlers.Birims.ValidationRules
{

    public class CreateBirimValidator : AbstractValidator<CreateBirimCommand>
    {
        public CreateBirimValidator()
        {
            RuleFor(x => x.KeyValue).NotEmpty();
            RuleFor(x => x.BirimAdi).NotEmpty();

        }
    }
    public class UpdateBirimValidator : AbstractValidator<UpdateBirimCommand>
    {
        public UpdateBirimValidator()
        {
            RuleFor(x => x.KeyValue).NotEmpty();
            RuleFor(x => x.BirimAdi).NotEmpty();

        }
    }
}