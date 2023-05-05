
using Business.Handlers.Rols.Commands;
using FluentValidation;

namespace Business.Handlers.Rols.ValidationRules
{

    public class CreateRolValidator : AbstractValidator<CreateRolCommand>
    {
        public CreateRolValidator()
        {
            RuleFor(x => x.KeyValue).NotEmpty();
            RuleFor(x => x.RolAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
    public class UpdateRolValidator : AbstractValidator<UpdateRolCommand>
    {
        public UpdateRolValidator()
        {
            RuleFor(x => x.KeyValue).NotEmpty();
            RuleFor(x => x.RolAdi).NotEmpty();
            RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
}