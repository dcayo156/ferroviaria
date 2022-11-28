using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.TagCategories.Queries.GetTagCategoryList
{
	public class GetTagCategoryListQueryHandler : IRequestHandler<GetTagCategoryListQuery, List<TagCategoryVm>>
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagCategoryVm>> Handle(GetTagCategoryListQuery request, CancellationToken cancellationToken)
        {
            var tagList = await _unitOfWork.TagCategoyRepository.GetTagCategoryListAsync();
        
            return _mapper.Map<List<TagCategoryVm>>(tagList);
        }
    }
}

