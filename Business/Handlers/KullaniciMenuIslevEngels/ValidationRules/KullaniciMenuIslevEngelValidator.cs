
using Business.Handlers.KullaniciMenuIslevEngels.Commands;
using FluentValidation;

namespace Business.Handlers.KullaniciMenuIslevEngels.ValidationRules
{

    public class CreateKullaniciMenuIslevEngelValidator : AbstractValidator<CreateKullaniciMenuIslevEngelCommand>
    {
        public CreateKullaniciMenuIslevEngelValidator()
        {
            RuleFor(x => x.MenuId).NotEmpty();
            RuleFor(x => x.KRMKLNKOD).NotEmpty();
            RuleFor(x => x.Durum).NotEmpty();

        }
    }
    public class UpdateKullaniciMenuIslevEngelValidator : AbstractValidator<UpdateKullaniciMenuIslevEngelCommand>
    {
        public UpdateKullaniciMenuIslevEngelValidator()
        {
            RuleFor(x => x.MenuId).NotEmpty();
            RuleFor(x => x.KRMKLNKOD).NotEmpty();
            RuleFor(x => x.Durum).NotEmpty();

        }
    }
}