
using Business.Handlers.BirimAgacKullaniciRols.Commands;
using FluentValidation;

namespace Business.Handlers.BirimAgacKullaniciRols.ValidationRules
{

    public class CreateBirimAgacKullaniciRolValidator : AbstractValidator<CreateBirimAgacKullaniciRolCommand>
    {
        public CreateBirimAgacKullaniciRolValidator()
        {

        }
    }
    public class UpdateBirimAgacKullaniciRolValidator : AbstractValidator<UpdateBirimAgacKullaniciRolCommand>
    {
        public UpdateBirimAgacKullaniciRolValidator()
        {

        }
    }
}