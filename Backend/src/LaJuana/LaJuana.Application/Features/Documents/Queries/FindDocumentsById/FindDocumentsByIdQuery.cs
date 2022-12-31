using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.FindDocumentsById
{
    public class FindDocumentsByIdQuery : IRequest<DocumentsFullVm>
    {
        public Guid Id { get; set; }

        public FindDocumentsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
