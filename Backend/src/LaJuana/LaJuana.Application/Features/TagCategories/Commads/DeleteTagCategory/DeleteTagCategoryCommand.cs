using System;
using MediatR;

namespace LaJuana.Application.Features.TagCategories.Commads.DeleteTagCategory
{
	public class DeleteTagCategoryCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}

