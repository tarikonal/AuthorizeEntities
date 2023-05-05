
using Business.Handlers.Kod_RolTips.Commands;
using FluentValidation;

namespace Business.Handlers.Kod_RolTips.ValidationRules
{

    public class CreateKod_RolTipValidator : AbstractValidator<CreateKod_RolTipCommand>
    {
        public CreateKod_RolTipValidator()
        {

        }
    }
    public class UpdateKod_RolTipValidator : AbstractValidator<UpdateKod_RolTipCommand>
    {
        public UpdateKod_RolTipValidator()
        {

        }
    }
}