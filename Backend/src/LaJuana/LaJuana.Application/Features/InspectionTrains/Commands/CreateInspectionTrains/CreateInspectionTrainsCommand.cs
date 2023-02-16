using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Commands.CreateInspectionTrains
{
    public class CreateInspectionTrainsCommand :IRequest<Guid>
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }
}
 