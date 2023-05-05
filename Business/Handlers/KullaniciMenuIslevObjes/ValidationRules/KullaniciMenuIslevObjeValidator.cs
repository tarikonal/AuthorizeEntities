
using Business.Handlers.KullaniciMenuIslevObjes.Commands;
using FluentValidation;

namespace Business.Handlers.KullaniciMenuIslevObjes.ValidationRules
{

    public class CreateKullaniciMenuIslevObjeValidator : AbstractValidator<CreateKullaniciMenuIslevObjeCommand>
    {
        public CreateKullaniciMenuIslevObjeValidator()
        {

        }
    }
    public class UpdateKullaniciMenuIslevObjeValidator : AbstractValidator<UpdateKullaniciMenuIslevObjeCommand>
    {
        public UpdateKullaniciMenuIslevObjeValidator()
        {

        }
    }
}