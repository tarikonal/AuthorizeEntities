
using Business.Handlers.BirimYetkiIslevObjes.Commands;
using FluentValidation;

namespace Business.Handlers.BirimYetkiIslevObjes.ValidationRules
{

    public class CreateBirimYetkiIslevObjeValidator : AbstractValidator<CreateBirimYetkiIslevObjeCommand>
    {
        public CreateBirimYetkiIslevObjeValidator()
        {

        }
    }
    public class UpdateBirimYetkiIslevObjeValidator : AbstractValidator<UpdateBirimYetkiIslevObjeCommand>
    {
        public UpdateBirimYetkiIslevObjeValidator()
        {

        }
    }
}