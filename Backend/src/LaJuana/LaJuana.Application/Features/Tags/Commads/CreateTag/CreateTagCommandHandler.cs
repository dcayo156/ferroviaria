using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Tags.Commads.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tagEntity = _mapper.Map<Tag>(request);
            var tagExist = (await _unitOfWork.Repository<Tag>().GetAsync(m => m.Name == request.Name)).FirstOrDefault();
            
            if (tagExist != null)
            {
                throw new Exception($"El tag : " + request.Name + " se encuentra registrado");
            }
            var tagcategory = (await _unitOfWork.Repository<TagCategory>().GetAsync(tc=>tc.Id==request.TagCategoryId)).FirstOrDefault();
            if (tagcategory == null)
            {
                throw new Exception($"El tag Category con ID: " + request.TagCategoryId.ToString() + " no se encuentra registrado");
            }
            _unitOfWork.TagRepository.AddEntity(tagEntity);
            var result = await _unitOfWork.Complete();
            if(result>0){
                
                _unitOfWork.TagRepository.AddEntityLucene(tagEntity,tagcategory);
            }
            return tagEntity.Id;
        }
    }
}

