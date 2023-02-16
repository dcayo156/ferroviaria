using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Queries.GetListInspectionTrainAll
{
    public class GetListInspectionTrainAllQuery : IRequest<DocumentFileVm>
    {
        public GetListInspectionTrainAllQuery()
        {

        }
    }
}
