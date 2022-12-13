using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Programs.Commands.CreatePrograms
{
    public class CreateProgramsCommandHandler : IRequestHandler<CreateProgramsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProgramsCommand> _logger;

        public CreateProgramsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateProgramsCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProgramsCommand request, CancellationToken cancellationToken)
        {
            var programEntity = _mapper.Map<Program>(request);

            _unitOfWork.Repository<Program>().AddEntity(programEntity);

            var result = await _unitOfWork.Complete();

            return programEntity.Id;
        }
    }
}
