using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.Programs.Queries.FindProgramsById;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LaJuana.Application.Features.Programs.Queries.FindProgramnsById
{
    public class FindProgramsByIdQueryHandler : IRequestHandler<FindProgramsByIdQuery, ProgramsFullVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FindProgramsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProgramsFullVm> Handle(FindProgramsByIdQuery request, CancellationToken cancellationToken)
        {
            var program = await _unitOfWork.ProgramRepository.FindByIdAsync(request.Id);

            var programFullVm = _mapper.Map<ProgramsFullVm>(program);

            using (var stream = System.IO.File.OpenRead(program.FilePath))
            {
                programFullVm.File = new FormFile(stream, 0, stream.Length, program.IconName, Path.GetFileName(stream.Name));              
            }
            return programFullVm;
        }

    }
}
