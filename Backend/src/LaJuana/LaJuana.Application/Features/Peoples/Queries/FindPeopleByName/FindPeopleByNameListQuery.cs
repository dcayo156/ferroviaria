using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.GetPeoples
{
	public class FindPeopleByNameListQuery : IRequest<List<PeopleVm>>
    {
        public string Name { get; set; } = String.Empty;

        public FindPeopleByNameListQuery(string username)
        {
            Name = username ?? throw new ArgumentNullException(nameof(username));
        }

    }
}

