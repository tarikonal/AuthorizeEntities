
using Business.Handlers.Menus.Commands;
using FluentValidation;

namespace Business.Handlers.Menus.ValidationRules
{

    public class CreateMenuValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuValidator()
        {
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();
            RuleFor(x => x.Sira).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty();
            RuleFor(x => x.IconText1).NotEmpty();
            RuleFor(x => x.IconText2).NotEmpty();
            RuleFor(x => x.IconText3).NotEmpty();

        }
    }
    public class UpdateMenuValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuValidator()
        {
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();
            RuleFor(x => x.Sira).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty();
            RuleFor(x => x.IconText1).NotEmpty();
            RuleFor(x => x.IconText2).NotEmpty();
            RuleFor(x => x.IconText3).NotEmpty();

        }
    }
}