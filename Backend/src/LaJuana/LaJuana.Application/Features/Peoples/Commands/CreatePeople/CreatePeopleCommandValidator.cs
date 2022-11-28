using FluentValidation;

namespace LaJuana.Application.Features.Peoples.Commands
{
    public class CreatePeopleCommandValidator : AbstractValidator<CreatePeopleCommand>
    {
        public CreatePeopleCommandValidator()
        {
            RuleFor(p => p.Person!.FirstName)
                .NotEmpty().WithMessage("{FirstName} no puede estar en blanco")
                .NotNull()
                .MaximumLength(50).WithMessage("{FirstName} no puede exceder los 50 caracteres");

            RuleFor(p => p.Person!.LastName)
                 .NotEmpty().WithMessage("La {LastName} no puede estar en blanco")
                 .NotNull()
                .MaximumLength(50).WithMessage("{LastName} no puede exceder los 50 caracteres");


        }
    }
}
