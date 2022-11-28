using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.FindTagByTagCategoryId
{
    public class FindTagByTagCategoryIdQueryHandler : IRequestHandler<FindTagByTagCategoryIdQuery, List<TagFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FindTagByTagCategoryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagFullVm>> Handle(FindTagByTagCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var tagList = await _unitOfWork.TagRepository.FindTagByCategoryIdAsync(request.Id);

            return _mapper.Map<List<TagFullVm>>(tagList);
        }
    }
}

