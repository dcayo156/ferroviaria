using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Commands.UpdateInspectionTrains
{
    public class UpdateInspectionTrainsCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public string SubCategoryId { get; set; } = string.Empty;
    }
}
