using LaJuana.Application.Features.Peoples.Queries.FindPeopleById;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Peoples.Queries.FindPeopleByNationalId
{
    public class FindPeopleByNationalIdQuery : IRequest<PeopleFullVm>
    {
        public string NationalId { get; set; }  = string.Empty;

        public FindPeopleByNationalIdQuery(string NationalId)
        {
            this.NationalId = NationalId;
        }
    }
}
