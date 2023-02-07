﻿using Aspose.Cells;
using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.Documents.Commands.CreateFileDocuments;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace LaJuana.Application.Features.InspectionTrains.Commands.CreateInspectionTrains
{
    public class CreateInspectionTrainsCommandHandler : IRequestHandler<CreateInspectionTrainsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDocumentsFileCommandHandler> _logger;
        private readonly IDocumentService _documentService;
        private readonly IAposeService _aposeService;
        public CreateInspectionTrainsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateDocumentsFileCommandHandler> logger,
            IDocumentService documentService,
            IAposeService aposeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _documentService = documentService;
            _aposeService = aposeService;
        }

        public async Task<Guid> Handle(CreateInspectionTrainsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.FileName == string.Empty) throw new Exception("File name se encuentra vacio");

                var pathFile = await _documentService.SaveDocument(
                                 request.SubCategoryId,
                                 request.FileName,
                                 request.File,
                                 true,
                                 true);
                var inspectionTrain = new InspectionTrain(); 

                inspectionTrain = await _aposeService.ReadDocInspectionIntegral(pathFile, inspectionTrain);

                inspectionTrain.Codigo = "Code";
                inspectionTrain.FileName = request.FileName;
                inspectionTrain.FilePath = pathFile;

                _unitOfWork.Repository<InspectionTrain>().AddEntity(inspectionTrain);
                await _unitOfWork.Complete();
                return inspectionTrain.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
