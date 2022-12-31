using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.FindDocumentsFileById
{
    public class FindDocumentsFileByIdQuery : IRequest<string>
    {
        public Guid Id { get; set; }
        public bool isFile { get; set; }

        public FindDocumentsFileByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
