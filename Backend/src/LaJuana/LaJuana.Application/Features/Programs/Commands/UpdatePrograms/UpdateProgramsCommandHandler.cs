using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Application.Features.Programs.Commands.CreatePrograms;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Programs.Commands.UpdatePrograms
{
    public class UpdateProgramsCommandHandler : IRequestHandler<UpdateProgramsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProgramsCommand> _logger;
        private readonly IDocumentService _directoryIconService;
        public UpdateProgramsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateProgramsCommand> logger,
            IDocumentService directoryIconService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _directoryIconService = directoryIconService;
        }

        public async Task<Unit> Handle(UpdateProgramsCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new Exception("El objeto es null"); }

            var filePath = await _directoryIconService.SaveIcon(request.IconName, request.File);

            request.FilePath = filePath;

            var ProgramsToUpdate = await _unitOfWork.Repository<Program>().GetByIdAsync(request.Id);
            if (ProgramsToUpdate == null)
            {
                _logger.LogError($"No se encontro el Program id {request.Id}");
                throw new NotFoundException(nameof(Program), request.Id);
            }
            _mapper.Map(request, ProgramsToUpdate, typeof(UpdateProgramsCommand), typeof(Program));

            _unitOfWork.Repository<Program>().UpdateEntity(ProgramsToUpdate);
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el Programs {request.Id}");
            return Unit.Value;
        }
    }
}
