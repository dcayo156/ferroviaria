using System;
using MediatR;

namespace LaJuana.Application.Features.TagCategories.Commads.CreateTagCategory
{
	public class CreateTagCategoryCommand : IRequest<Guid>
	{
		public string Description { get; set; } = string.Empty;
	}
}

