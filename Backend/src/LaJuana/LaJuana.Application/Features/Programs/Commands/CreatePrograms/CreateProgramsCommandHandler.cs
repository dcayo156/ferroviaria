using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models;
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
        private readonly IHelpersDocument _helpersDocument;
        //private readonly DirectoryIconSettings _directoryIconSettings;
        public CreateProgramsCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateProgramsCommand> logger,
            IHelpersDocument helpersDocument)
        //DirectoryIconSettings directoryIconSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _helpersDocument =helpersDocument;
            //_directoryIconSettings = directoryIconSettings; 
        }

        public async Task<Guid> Handle(CreateProgramsCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new Exception("El objeto es null"); }

            var base64 = request.File.Split(',')[1];

            var directoryPath = "C:\\Programs\\Icon";

            var filePath = directoryPath + "\\" + request.IconName;

            _helpersDocument.CheckDirectory(directoryPath);

            await _helpersDocument.SaveFile(base64, filePath);           

            request.FilePath = filePath;

            var programEntity = _mapper.Map<Program>(request);

            _unitOfWork.Repository<Program>().AddEntity(programEntity);

            var result = await _unitOfWork.Complete();

            return programEntity.Id;
        }
    }
}
