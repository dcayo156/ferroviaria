using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.Programs.Queries.FindPromansFileById
{
    public class FindProgramsFileByIdQueryHandler : IRequestHandler<FindProgramsFileByIdQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FindProgramsFileByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> Handle(FindProgramsFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var program = await _unitOfWork.ProgramRepository.FindByIdAsync(request.Id);

                var programFullVm = _mapper.Map<ProgramsFullVm>(program);
                Byte[] bytes = File.ReadAllBytes(program.FilePath);
                String file = Convert.ToBase64String(bytes);
                return file;
            }
            catch (Exception)
            {
                throw new Exception("Error al leer el File");
            }
           
        }
    }
}
