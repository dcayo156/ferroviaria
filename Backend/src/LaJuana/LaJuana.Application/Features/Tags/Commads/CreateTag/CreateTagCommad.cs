using System;
using MediatR;

namespace LaJuana.Application.Features.Tags.Commads.CreateTag
{
	public class CreateTagCommand : IRequest<Guid>
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Guid TagCategoryId { get; set; }
	}
}

