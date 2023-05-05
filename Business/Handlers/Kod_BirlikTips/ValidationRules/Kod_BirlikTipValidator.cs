
using Business.Handlers.Kod_BirlikTips.Commands;
using FluentValidation;

namespace Business.Handlers.Kod_BirlikTips.ValidationRules
{

    public class CreateKod_BirlikTipValidator : AbstractValidator<CreateKod_BirlikTipCommand>
    {
        public CreateKod_BirlikTipValidator()
        {

        }
    }
    public class UpdateKod_BirlikTipValidator : AbstractValidator<UpdateKod_BirlikTipCommand>
    {
        public UpdateKod_BirlikTipValidator()
        {

        }
    }
}