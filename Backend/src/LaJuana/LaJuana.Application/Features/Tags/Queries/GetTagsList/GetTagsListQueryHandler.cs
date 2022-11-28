using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.GetTagsList
{
	public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, List<TagFullVm>>
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagFullVm>> Handle(GetTagsListQuery request, CancellationToken cancellationToken)
        {
            var tagList = await _unitOfWork.TagRepository.GetTagsListAsync();

            return _mapper.Map<List<TagFullVm>>(tagList);
        }
    }
}

