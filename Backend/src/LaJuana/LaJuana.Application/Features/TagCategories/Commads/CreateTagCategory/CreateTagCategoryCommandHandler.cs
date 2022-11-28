using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.TagCategories.Commads.CreateTagCategory
{
    public class CreateTagCategoryCommandHandler : IRequestHandler<CreateTagCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTagCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTagCategoryCommand request, CancellationToken cancellationToken)
        {
            var tagCategoryEntity = _mapper.Map<TagCategory>(request);
    
            var tagcategory = (await _unitOfWork.Repository<TagCategory>().GetAsync(tc=>tc.Description==request.Description)).FirstOrDefault();
            if (tagcategory != null)
            {
                throw new Exception($"El Tag Category con descripcion: " + request.Description.ToString() + " se encuentra registrado");
            }
            _unitOfWork.TagCategoyRepository.AddEntity(tagCategoryEntity);
            var result = await _unitOfWork.Complete();
            if(result>0){
                
            }
            return tagCategoryEntity.Id;
        }
    }
}

