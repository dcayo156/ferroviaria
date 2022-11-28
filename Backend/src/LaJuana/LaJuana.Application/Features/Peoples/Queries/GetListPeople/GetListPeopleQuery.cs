using System;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Peoples.Queries.GetListPeople
{
    public class GetListPeopleQuery : IRequest<List<PeopleFullVm>>
    {


        public GetListPeopleQuery()
        {
            
        }

    }
}

