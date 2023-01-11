using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Categories.Queries.GetListChildrenCategories
{
    public class GetListChildrenCategoriesQueryHandler : IRequestHandler<GetListChildrenCategoriesQuery, List<CategoriesChildrenFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetListChildrenCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoriesChildrenFullVm>> Handle(GetListChildrenCategoriesQuery request, CancellationToken cancellationToken)
        {
            var parentCaegories = await _unitOfWork.CategoryRepository.GetListCategories();
            parentCaegories = parentCaegories.Where(c => c.ParentCategoryId == null).ToList();
            foreach (var item in parentCaegories)
            {
                var subCategories = await _unitOfWork.CategoryRepository.FindByIdSubCategoryAsync(item.Id);
                item.Categories = subCategories ?? null;
            }
            return _mapper.Map<List<CategoriesChildrenFullVm>>(parentCaegories);
        }
    }
}
