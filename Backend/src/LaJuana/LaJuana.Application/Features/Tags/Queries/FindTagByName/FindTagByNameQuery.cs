using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.FindTagByName
{
	public class FindTagByNameQuery : IRequest<List<TagFullVm>>
	{
        public string Name { get; set; } = String.Empty;

        public FindTagByNameQuery(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}

