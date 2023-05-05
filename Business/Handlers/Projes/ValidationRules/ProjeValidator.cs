
using Business.Handlers.Projes.Commands;
using FluentValidation;

namespace Business.Handlers.Projes.ValidationRules
{

    public class CreateProjeValidator : AbstractValidator<CreateProjeCommand>
    {
        public CreateProjeValidator()
        {
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();
            RuleFor(x => x.UrlAdresi).NotEmpty();
            RuleFor(x => x.BakimUrlAdresi).NotEmpty();
            RuleFor(x => x.Logo).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty();
            RuleFor(x => x.IconText1).NotEmpty();
            RuleFor(x => x.IconText2).NotEmpty();
            RuleFor(x => x.IconText3).NotEmpty();
            RuleFor(x => x.Ico).NotEmpty();
            RuleFor(x => x.KullanimKlavuzu).NotEmpty();

        }
    }
    public class UpdateProjeValidator : AbstractValidator<UpdateProjeCommand>
    {
        public UpdateProjeValidator()
        {
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();
            RuleFor(x => x.UrlAdresi).NotEmpty();
            RuleFor(x => x.BakimUrlAdresi).NotEmpty();
            RuleFor(x => x.Logo).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty();
            RuleFor(x => x.IconText1).NotEmpty();
            RuleFor(x => x.IconText2).NotEmpty();
            RuleFor(x => x.IconText3).NotEmpty();
            RuleFor(x => x.Ico).NotEmpty();
            RuleFor(x => x.KullanimKlavuzu).NotEmpty();

        }
    }
}