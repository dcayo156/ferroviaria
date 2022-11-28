using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.GetTagsWithAddressCountOfPerson
{
	public class GetTagsWithAddressCountOfPersonQuery : IRequest<List<CategoryWithTagsVm>>
	{
		public GetTagsWithAddressCountOfPersonQuery()
		{			
		}
	}
}

