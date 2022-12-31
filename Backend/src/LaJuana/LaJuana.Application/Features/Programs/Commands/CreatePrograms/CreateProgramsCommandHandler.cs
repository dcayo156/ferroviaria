using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LaJuana.Application.Features.Programs.Commands.CreatePrograms
{
    public class CreateProgramsCommandHandler : IRequestHandler<CreateProgramsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProgramsCommand> _logger;
        private readonly IDocumentService _directoryIconService;
        public CreateProgramsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateProgramsCommand> logger,
            IDocumentService directoryIconService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _directoryIconService = directoryIconService; 
        }

        public async Task<Guid> Handle(CreateProgramsCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new Exception("El objeto es null"); }

            var filePath = await _directoryIconService.SaveIcon(request.IconName, request.File);                   

            request.FilePath = filePath;

            var programEntity = _mapper.Map<Program>(request);

            _unitOfWork.Repository<Program>().AddEntity(programEntity);

            var result = await _unitOfWork.Complete();

            return programEntity.Id;
        }
    }
}
