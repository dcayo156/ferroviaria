using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.InspectionTrains.Queries.GetListInspectionTrains
{
    public class GetListInspectionTrainsQuery : IRequest<List<InspectionTrainsFullVm>>
    {
        public GetListInspectionTrainsQuery()
        {

        }
    }
}
