using System;
using FluentValidation;

namespace LaJuana.Application.Features.TagCategories.Commads.CreateTagCategory
{
	public class CreateTagCommandValidator : AbstractValidator<CreateTagCategoryCommand>
	{
		public CreateTagCommandValidator()
		{
			RuleFor(p => p.Description)
				.NotEmpty().WithMessage("{Description} no puede estar en blanco")
				.NotNull()
				.MaximumLength(50).WithMessage("{Description} no puede exceder los 50 caracteres");
		}
	}
}

