using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Commands.CreateInspectionTrains
{
    public class CreateInspectionTrainsCommand :IRequest<Guid>
    {
        public string FileName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public string SubCategoryId { get; set; } = string.Empty;
    }
}
 