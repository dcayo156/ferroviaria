using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.InspectionTrains.Queries.GetListInspectionTrainAll
{
    public class GetListInspectionTrainsAllQueryHandler : IRequestHandler<GetListInspectionTrainAllQuery, DocumentFileVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IAposeService _aposeService;
        private readonly IMineTypeService _mineType;
        public GetListInspectionTrainsAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IAposeService aposeService, IMineTypeService mineType)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _aposeService = aposeService;
            _mineType = mineType;
        }


        public async Task<DocumentFileVm> Handle(GetListInspectionTrainAllQuery request, CancellationToken cancellationToken)
        {
            var listInspeccionTren = await _unitOfWork.InspectionTrainsRepository.GetListInspectionTrains();
            var filePath = await  _aposeService.SaveDocInspectionIntegral("",listInspeccionTren.ToList());

            
            var mimeType = _mineType.GetMimeType(filePath);
            Byte[] bytes = File.ReadAllBytes(filePath);
            String file = Convert.ToBase64String(bytes);

            var documentFileVm = new DocumentFileVm()
            {
                File = file,
                MimeType = mimeType,
                FilePath = filePath,
                FileName = "ListaInspeccionTren.xlsx"
            };
            return documentFileVm;
        }
    }
}
