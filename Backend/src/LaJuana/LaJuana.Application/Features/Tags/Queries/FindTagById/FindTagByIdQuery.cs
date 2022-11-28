using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.FindTagById
{
	public class FindTagByIdQuery : IRequest<TagFullVm>
	{
		public Guid Id { get; set; }

        public FindTagByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}

