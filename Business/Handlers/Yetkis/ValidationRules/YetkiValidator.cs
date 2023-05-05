
using Business.Handlers.Yetkis.Commands;
using FluentValidation;

namespace Business.Handlers.Yetkis.ValidationRules
{

    public class CreateYetkiValidator : AbstractValidator<CreateYetkiCommand>
    {
        public CreateYetkiValidator()
        {
            RuleFor(x => x.YetkiAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
    public class UpdateYetkiValidator : AbstractValidator<UpdateYetkiCommand>
    {
        public UpdateYetkiValidator()
        {
            RuleFor(x => x.YetkiAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
}