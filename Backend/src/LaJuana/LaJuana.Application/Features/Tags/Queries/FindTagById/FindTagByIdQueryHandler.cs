using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.FindTagById
{
	public class FindTagByIdQueryHandler : IRequestHandler<FindTagByIdQuery, TagFullVm>
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FindTagByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TagFullVm> Handle(FindTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _unitOfWork.TagRepository.FindTagByIdAsync(request.Id);

            return _mapper.Map<TagFullVm>(tag);
        }
    }
}

