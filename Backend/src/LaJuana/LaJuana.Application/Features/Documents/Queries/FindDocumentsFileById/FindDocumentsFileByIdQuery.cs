using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.FindDocumentsFileById
{
    public class FindDocumentsFileByIdQuery : IRequest<DocumentFileVm>
    {
        public Guid Id { get; set; }
        public bool isFile { get; set; }

        public FindDocumentsFileByIdQuery(Guid id, Boolean isfile)
        {
            Id = id;
            isFile = isfile;
        }
    }
}
