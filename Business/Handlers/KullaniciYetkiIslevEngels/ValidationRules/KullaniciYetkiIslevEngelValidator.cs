
using Business.Handlers.KullaniciYetkiIslevEngels.Commands;
using FluentValidation;

namespace Business.Handlers.KullaniciYetkiIslevEngels.ValidationRules
{

    public class CreateKullaniciYetkiIslevEngelValidator : AbstractValidator<CreateKullaniciYetkiIslevEngelCommand>
    {
        public CreateKullaniciYetkiIslevEngelValidator()
        {
            RuleFor(x => x.YetkiId).NotEmpty();
            RuleFor(x => x.KRMKLNKOD).NotEmpty();
            RuleFor(x => x.Durum).NotEmpty();

        }
    }
    public class UpdateKullaniciYetkiIslevEngelValidator : AbstractValidator<UpdateKullaniciYetkiIslevEngelCommand>
    {
        public UpdateKullaniciYetkiIslevEngelValidator()
        {
            RuleFor(x => x.YetkiId).NotEmpty();
            RuleFor(x => x.KRMKLNKOD).NotEmpty();
            RuleFor(x => x.Durum).NotEmpty();

        }
    }
}