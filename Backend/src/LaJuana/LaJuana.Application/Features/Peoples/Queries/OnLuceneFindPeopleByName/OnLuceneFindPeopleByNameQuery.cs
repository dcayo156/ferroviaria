using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Lucene.FindUserByNameLucene
{
	public class OnLuceneFindPeopleByNameQuery : IRequest<List<PeopleVm>>
	{
        public string Name { get; set; } = String.Empty;

        public OnLuceneFindPeopleByNameQuery(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}

