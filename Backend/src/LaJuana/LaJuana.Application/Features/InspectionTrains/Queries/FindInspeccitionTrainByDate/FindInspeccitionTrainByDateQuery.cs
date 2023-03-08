using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Queries.FindInspeccitionTrainByDate
{
    public class FindInspeccitionTrainByDateQuery : IRequest<InspectionTrainPieChartFullVm>
    {
    }
}
