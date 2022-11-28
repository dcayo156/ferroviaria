using System;
using MediatR;

namespace LaJuana.Application.Features.Tags.Commads.DeleteTag
{
	public class DeleteTagCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}

