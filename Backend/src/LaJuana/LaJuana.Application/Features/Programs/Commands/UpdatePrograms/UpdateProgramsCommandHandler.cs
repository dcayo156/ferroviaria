using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Programs.Commands.UpdatePrograms
{
    public class UpdateProgramsCommandHandler : IRequestHandler<UpdateProgramsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProgramsCommand> _logger;
        public UpdateProgramsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProgramsCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProgramsCommand request, CancellationToken cancellationToken)
        {

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
