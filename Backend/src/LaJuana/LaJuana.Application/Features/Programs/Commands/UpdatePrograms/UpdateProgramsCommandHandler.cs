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
        private readonly IHelpersDocument _helpersDocument;
        public UpdateProgramsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateProgramsCommand> logger,
            IHelpersDocument helpersDocument)      
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _helpersDocument = helpersDocument;            
        }      

        public async Task<Unit> Handle(UpdateProgramsCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new Exception("El objeto es null"); }

            var base64 = request.File.Split(',')[1];

            var directoryPath = "C:\\Programs\\Icon";

            var filePath = directoryPath + "\\" + request.IconName;

            _helpersDocument.CheckDirectory(directoryPath);

            await _helpersDocument.SaveFile(base64, filePath);

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
