using MediatR;

namespace LaJuana.Application.Features.Documents.Commands.DeleteDocuments
{
    public class DeleteDocumentsCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
