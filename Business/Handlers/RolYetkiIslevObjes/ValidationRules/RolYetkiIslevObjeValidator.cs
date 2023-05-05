
using Business.Handlers.RolYetkiIslevObjes.Commands;
using FluentValidation;

namespace Business.Handlers.RolYetkiIslevObjes.ValidationRules
{

    public class CreateRolYetkiIslevObjeValidator : AbstractValidator<CreateRolYetkiIslevObjeCommand>
    {
        public CreateRolYetkiIslevObjeValidator()
        {

        }
    }
    public class UpdateRolYetkiIslevObjeValidator : AbstractValidator<UpdateRolYetkiIslevObjeCommand>
    {
        public UpdateRolYetkiIslevObjeValidator()
        {

        }
    }
}