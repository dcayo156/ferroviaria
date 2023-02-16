using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Queries.FindDocumentsFileById
{
    public class FindDocumentsFileByIdQueryHandler : IRequestHandler<FindDocumentsFileByIdQuery, DocumentFileVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMineTypeService _mineType;
        public FindDocumentsFileByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMineTypeService mineType)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mineType = mineType;
        }

        public async Task<DocumentFileVm> Handle(FindDocumentsFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var document = await _unitOfWork.DocumentRepository.FindByIdAsync(request.Id);
                var filePath = request.isFile? document.FilePath : document.PhotoPath;
                var mimeType = _mineType.GetMimeType(filePath);
                Byte[] bytes = File.ReadAllBytes(filePath);
                String file = Convert.ToBase64String(bytes);

                var documentFileVm = new DocumentFileVm()
                {
                    File = file,
                    MimeType = mimeType,
                    FilePath = filePath,
                    FileName = request.isFile ? document.FileName : document.PhotoName,
                };
                return documentFileVm;
            }
            catch (Exception)
            {
                throw new Exception("Error al leer el File");
            }
        }
    
    }
}
