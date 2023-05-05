
using Business.Handlers.Kod_RolSeviyes.Commands;
using FluentValidation;

namespace Business.Handlers.Kod_RolSeviyes.ValidationRules
{

    public class CreateKod_RolSeviyeValidator : AbstractValidator<CreateKod_RolSeviyeCommand>
    {
        public CreateKod_RolSeviyeValidator()
        {
            RuleFor(x => x.SeviyeKodu).NotEmpty();

        }
    }
    public class UpdateKod_RolSeviyeValidator : AbstractValidator<UpdateKod_RolSeviyeCommand>
    {
        public UpdateKod_RolSeviyeValidator()
        {
            RuleFor(x => x.SeviyeKodu).NotEmpty();

        }
    }
}