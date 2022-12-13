using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Programs.Queries.GetListPrograms
{
    public class GetListPorgramsQueryHandler : IRequestHandler<GetListProgramsQuery, List<ProgramsFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly IMapper _mapper;
        public GetListPorgramsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProgramsFullVm>> Handle(GetListProgramsQuery request, CancellationToken cancellationToken)
        {
            var programList = await _unitOfWork.ProgramRepository.GetListPrograms();

            return _mapper.Map<List<ProgramsFullVm>>(programList);
        }
    }
}
