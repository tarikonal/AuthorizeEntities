
using Business.Handlers.Islevs.Commands;
using FluentValidation;

namespace Business.Handlers.Islevs.ValidationRules
{

    public class CreateIslevValidator : AbstractValidator<CreateIslevCommand>
    {
        public CreateIslevValidator()
        {
            RuleFor(x => x.IslevAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
    public class UpdateIslevValidator : AbstractValidator<UpdateIslevCommand>
    {
        public UpdateIslevValidator()
        {
            RuleFor(x => x.IslevAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
}