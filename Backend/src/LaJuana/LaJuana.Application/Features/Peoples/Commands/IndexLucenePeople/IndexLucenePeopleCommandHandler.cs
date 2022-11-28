using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;
using LaJuana.Domain;
namespace LaJuana.Application.Features.Peoples.Commands.IndexLucenePeople
{
	public class IndexLucenePeopleCommandHandler : IRequestHandler<IndexLucenePeopleCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<IndexLucenePeopleCommandHandler> _logger;

        public IndexLucenePeopleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IndexLucenePeopleCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(IndexLucenePeopleCommand request, CancellationToken cancellationToken)
        {
            //indexacion Peoples
            _unitOfWork.PeopleRepository.DeleteIndexLucene();
            var userList = await _unitOfWork.PeopleRepository.FindUserAll();
            _unitOfWork.PeopleRepository.AddEntitiesLucene(userList);
            //indexacion tags
            _unitOfWork.TagRepository.DeleteIndexLucene();
            var tagList = await _unitOfWork.TagRepository.GetTagsListAsync();
            _unitOfWork.TagRepository.AddEntitiesLucene(tagList);
            foreach(People p in userList){
                foreach(Tag tag in p.Tags){
                    _unitOfWork.TagRepository.AddPersonToTagInLucene(p,tag);
                }
            }
            return Unit.Value;
        }

    }
}

