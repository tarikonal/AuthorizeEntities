
using Business.Handlers.KullaniciYetkiIslevObjes.Commands;
using FluentValidation;

namespace Business.Handlers.KullaniciYetkiIslevObjes.ValidationRules
{

    public class CreateKullaniciYetkiIslevObjeValidator : AbstractValidator<CreateKullaniciYetkiIslevObjeCommand>
    {
        public CreateKullaniciYetkiIslevObjeValidator()
        {

        }
    }
    public class UpdateKullaniciYetkiIslevObjeValidator : AbstractValidator<UpdateKullaniciYetkiIslevObjeCommand>
    {
        public UpdateKullaniciYetkiIslevObjeValidator()
        {

        }
    }
}