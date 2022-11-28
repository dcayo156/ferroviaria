using FluentValidation;

namespace LaJuana.Application.Features.PersonTags.Commands
{
    public class AddTagToPersonCommandValidator : AbstractValidator<AddTagToPersonCommand>
    {
        public AddTagToPersonCommandValidator()
        {
            RuleFor(addTagToPersonCommand => addTagToPersonCommand.PersonId)
            .NotEmpty().WithMessage("Error, value PersonaId is required");

            RuleFor(addTagToPersonCommand => addTagToPersonCommand.TagId)
            .NotEmpty().WithMessage("Error, value TagId is required");

        }
    }
}
