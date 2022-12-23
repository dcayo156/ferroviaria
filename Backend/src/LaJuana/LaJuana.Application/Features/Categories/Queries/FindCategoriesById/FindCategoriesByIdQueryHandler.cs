using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.Programs.Queries.FindProgramsById;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LaJuana.Application.Features.Categories.Queries.FindCategoriesById
{
    public class FindCategoriesByIdQueryHandler : IRequestHandler<FindCategoriesByIdQuery, CategoriesFullVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FindCategoriesByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoriesFullVm> Handle(FindCategoriesByIdQuery request, CancellationToken cancellationToken)
        {
            var program = await _unitOfWork.CategoryRepository.FindByIdAsync(request.Id);

            var programFullVm = _mapper.Map<CategoriesFullVm>(program);
                       
            return programFullVm;
        }

    }
}
