using System;
using MediatR;

namespace LaJuana.Application.Features.TagCategories.Commads.UpdateTagCategory
{
	public class UpdateTagCategoryCommand : IRequest
	{
		public Guid Id { get; set; }
		public string Description { get; set; } = string.Empty;
	}
}

