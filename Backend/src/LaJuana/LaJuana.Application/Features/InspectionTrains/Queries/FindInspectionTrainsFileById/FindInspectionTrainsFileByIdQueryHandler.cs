using Aspose.Cells;
using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.InspectionTrains.Queries.FindInspectionTrainsFileById;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.InspectionTrains.Queries.FindInspectionTrainsFileById
{
    public class FindInspectionTrainsFileByIdQueryHandler : IRequestHandler<FindInspectionTrainsFileByIdQuery, DocumentFileVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMineTypeService _mineType;
        public FindInspectionTrainsFileByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMineTypeService mineType)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mineType = mineType;
        }

        public async Task<DocumentFileVm> Handle(FindInspectionTrainsFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {               
                var document = await _unitOfWork.InspectionTrainsRepository.FindByIdAsync(request.Id);
                var mimeType = _mineType.GetMimeType(document.FilePath);
                Byte[] bytes = File.ReadAllBytes(document.FilePath);
                String file = Convert.ToBase64String(bytes);

                var documentFileVm = new DocumentFileVm()
                {
                    File = file,
                    MimeType = mimeType,
                    FilePath = document.FilePath,
                    FileName = document.FileName
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
