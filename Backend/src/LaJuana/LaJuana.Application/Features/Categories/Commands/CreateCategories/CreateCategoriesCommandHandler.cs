﻿using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Categories.Commands.CreateCategories
{
    public class CreateCategoriesCommandHandler : IRequestHandler<CreateCategoriesCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoriesCommand> _logger;
       
        public CreateCategoriesCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateCategoriesCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateCategoriesCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new Exception("El objeto para dar alta la Categoria es null"); }

            if (request.ParentCategoryId != null)
            {
                var parentCategory = await _unitOfWork.CategoryRepository.FindByIdAsync(request.ParentCategoryId.Value);
                if (parentCategory == null) 
                {
                    _logger.LogError($"No se encontro el Parent Category {request.ParentCategoryId.Value}");
                    throw new Exception($"No se encontro el Parent Category {request.ParentCategoryId.Value}");
                }
            }

            var categoryEntity = _mapper.Map<Category>(request);

            _unitOfWork.Repository<Category>().AddEntity(categoryEntity);

            var result = await _unitOfWork.Complete();

            return categoryEntity.Id;
        }
    }
}
