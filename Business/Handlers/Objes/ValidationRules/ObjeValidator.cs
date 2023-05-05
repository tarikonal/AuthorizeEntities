
using Business.Handlers.Objes.Commands;
using FluentValidation;

namespace Business.Handlers.Objes.ValidationRules
{

    public class CreateObjeValidator : AbstractValidator<CreateObjeCommand>
    {
        public CreateObjeValidator()
        {
            RuleFor(x => x.ObjeAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
    public class UpdateObjeValidator : AbstractValidator<UpdateObjeCommand>
    {
        public UpdateObjeValidator()
        {
            RuleFor(x => x.ObjeAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
}