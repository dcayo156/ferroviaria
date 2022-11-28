using System;
using MediatR;

namespace LaJuana.Application.Features.Tags.Commads.UpdateTag
{
	public class UpdateTagCommand : IRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Guid? TagCategoryId { get; set; }
	}
}

