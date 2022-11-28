using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.GetTagsList
{
	public class GetTagsListQuery : IRequest<List<TagFullVm>>
	{
		public GetTagsListQuery()
		{			
		}
	}
}

