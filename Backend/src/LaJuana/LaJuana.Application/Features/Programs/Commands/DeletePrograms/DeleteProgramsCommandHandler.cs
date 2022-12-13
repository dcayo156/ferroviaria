using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Programs.Commands.DeletePrograms
{
    public class DeleteProgramsCommandHandler : IRequestHandler<DeleteProgramsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProgramsCommandHandler> _logger;

        public DeleteProgramsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteProgramsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProgramsCommand request, CancellationToken cancellationToken)
        {
            var ProgramsToDelete = await _unitOfWork.Repository<Program>().GetByIdAsync(request.Id);
            if (ProgramsToDelete == null)
            {
                _logger.LogError($"{request.Id} Programs no existe en el sistema");
                throw new NotFoundException(nameof(Program), request.Id);
            }
            _unitOfWork.Repository<Program>().DeleteEntity(ProgramsToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Programs fue eliminado con exito");

            return Unit.Value;
        }
    }
}