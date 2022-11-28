﻿using System;
using FluentValidation;

namespace LaJuana.Application.Features.Tags.Commads.UpdateTag
{
    public class UpdateTagCategoryCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCategoryCommandValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{Name} no puede estar en blanco")
            .NotNull()
            .MaximumLength(50).WithMessage("{Name} no puede exceder los 50 caracteres");
        }
    }
}

