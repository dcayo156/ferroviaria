using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Categories.Queries.GetListCategories
{
    public class GetListCategoriesQueryHandler : IRequestHandler<GetListCategoriesQuery, List<CategoriesFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;        
        private readonly IMapper _mapper;
        public GetListCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoriesFullVm>> Handle(GetListCategoriesQuery request, CancellationToken cancellationToken)
        {
            var programList = await _unitOfWork.CategoryRepository.GetListCategories();

            return _mapper.Map<List<CategoriesFullVm>>(programList);
        }
    }
}
