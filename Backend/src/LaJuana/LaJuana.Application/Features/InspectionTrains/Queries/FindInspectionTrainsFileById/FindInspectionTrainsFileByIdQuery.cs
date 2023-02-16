using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Queries.FindInspectionTrainsFileById
{
    public class FindInspectionTrainsFileByIdQuery : IRequest<DocumentFileVm>
    {
        public Guid Id { get; set; }
        public FindInspectionTrainsFileByIdQuery(Guid id) => Id =id;
    }
}
