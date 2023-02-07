using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Commands.DeleteInspectionTrains
{
    public class DeleteInspectionTrainsCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
