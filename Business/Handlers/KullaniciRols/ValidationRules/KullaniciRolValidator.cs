
using Business.Handlers.KullaniciRols.Commands;
using FluentValidation;

namespace Business.Handlers.KullaniciRols.ValidationRules
{

    public class CreateKullaniciRolValidator : AbstractValidator<CreateKullaniciRolCommand>
    {
        public CreateKullaniciRolValidator()
        {

        }
    }
    public class UpdateKullaniciRolValidator : AbstractValidator<UpdateKullaniciRolCommand>
    {
        public UpdateKullaniciRolValidator()
        {

        }
    }
}