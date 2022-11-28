using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.FindTagByTagCategoryId
{
	public class FindTagByTagCategoryIdQuery : IRequest<List<TagFullVm>>
	{
		public Guid Id { get; set; }

		public FindTagByTagCategoryIdQuery(Guid id)
		{
			Id = id;
		}
	}
}

