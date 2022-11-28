using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.TagCategories.Queries.GetTagCategoryList
{
	public class GetTagCategoryListQuery : IRequest<List<TagCategoryVm>>
	{
		public GetTagCategoryListQuery()
		{			
		}
	}
}

