using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.FindDocumentsFileById
{
    public class FindDocumentsFileByIdQueryHandler : IRequestHandler<FindDocumentsFileByIdQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FindDocumentsFileByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> Handle(FindDocumentsFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var document = await _unitOfWork.DocumentRepository.FindByIdAsync(request.Id);
                var filePath = request.isFile ? document.FilePath : document.PhotoPath;
                Byte[] bytes = File.ReadAllBytes(filePath);
                String file = Convert.ToBase64String(bytes);
                return file;
            }
            catch (Exception)
            {
                throw new Exception("Error al leer el File");
            }

        }
    }
}
