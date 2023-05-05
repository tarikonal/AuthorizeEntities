
using Business.Handlers.BirimAgacs.Commands;
using FluentValidation;

namespace Business.Handlers.BirimAgacs.ValidationRules
{

    public class CreateBirimAgacValidator : AbstractValidator<CreateBirimAgacCommand>
    {
        public CreateBirimAgacValidator()
        {

        }
    }
    public class UpdateBirimAgacValidator : AbstractValidator<UpdateBirimAgacCommand>
    {
        public UpdateBirimAgacValidator()
        {

        }
    }
}