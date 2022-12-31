using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.GetListDocuments
{
    public class GetListDocumentsQueryHandler : IRequestHandler<GetListDocumentsQuery, List<DocumentsFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly IMapper _mapper;
        public GetListDocumentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<DocumentsFullVm>> Handle(GetListDocumentsQuery request, CancellationToken cancellationToken)
        {
            var programList = await _unitOfWork.DocumentRepository.GetListDocuments();

            return _mapper.Map<List<DocumentsFullVm>>(programList);
        }
    }
}
