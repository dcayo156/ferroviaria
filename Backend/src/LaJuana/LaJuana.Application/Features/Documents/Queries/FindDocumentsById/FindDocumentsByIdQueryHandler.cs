using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.FindDocumentsById
{
    public class FindDocumentsByIdQueryHandler : IRequestHandler<FindDocumentsByIdQuery, DocumentsFullVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FindDocumentsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DocumentsFullVm> Handle(FindDocumentsByIdQuery request, CancellationToken cancellationToken)
        {
            var program = await _unitOfWork.DocumentRepository.FindByIdAsync(request.Id);

            var programFullVm = _mapper.Map<DocumentsFullVm>(program);
                       
            return programFullVm;
        }

    }
}
