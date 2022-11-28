using FluentValidation;

namespace LaJuana.Application.Features.PersonTags.Commands
{
    public class DeleteTagToPersonCommandValidator : AbstractValidator<DeleteTagToPersonCommand>
    {
        public DeleteTagToPersonCommandValidator()
        {
            RuleFor(addTagToPersonCommand => addTagToPersonCommand.PersonId)
            .NotEmpty().WithMessage("Error, value PersonaId is required");

            RuleFor(addTagToPersonCommand => addTagToPersonCommand.TagId)
            .NotEmpty().WithMessage("Error, value TagId is required");

        }
    }
}
