using MediatR;

namespace LaJuana.Application.Features.Documents.Commands.CreateDocuments
{
    public class CreateDocumentsCommand : IRequest<Guid>
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;      
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
    }
}
