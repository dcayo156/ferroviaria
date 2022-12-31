using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.GetListDocuments
{
    public class GetListDocumentsQuery : IRequest<List<DocumentsFullVm>>
    {
        public GetListDocumentsQuery()
        {

        }

    }
}
