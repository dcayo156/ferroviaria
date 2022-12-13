﻿using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.Programs.Queries.FindProgramsById;
using LaJuana.Application.Models.ViewModels;
using MediatR;

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
            var programList = await _unitOfWork.ProgramRepository.FindByIdAsync(request.Id);

            return _mapper.Map<ProgramsFullVm>(programList);
        }

    }
}
