using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.FindTagByName
{
	public class FindTagByNameQueryHandler : IRequestHandler<FindTagByNameQuery, List<TagFullVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FindTagByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagFullVm>> Handle(FindTagByNameQuery request, CancellationToken cancellationToken)
        {
            var tagList = await _unitOfWork.TagRepository.FindTagByNameAsync(request.Name);

            return _mapper.Map<List<TagFullVm>>(tagList);
        }
    }
}

