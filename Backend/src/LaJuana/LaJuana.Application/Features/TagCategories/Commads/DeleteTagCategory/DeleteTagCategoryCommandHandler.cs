using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.TagCategories.Commads.DeleteTagCategory
{
    public class DeleteTagCategoryCommandHandler : IRequestHandler<DeleteTagCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteTagCategoryCommand> _logger;

        public DeleteTagCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteTagCategoryCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTagCategoryCommand request, CancellationToken cancellationToken)
        {

            // ask if exist tag
            var tagToDelete = await _unitOfWork.TagCategoyRepository.GetByIdAsync(request.Id);
            if (tagToDelete == null)
            {
                _logger.LogError($"{request.Id} tag no existe en el sistema");
                throw new NotFoundException(nameof(Tag), request.Id);
            }
            var tagList = await _unitOfWork.TagRepository.FindTagByCategoryIdAsync(request.Id);
            if(tagList.Count() > 0){
                _logger.LogError($"{request.Id} tag no existe en el sistema");
                throw new Exception("La categoria tiene tags asignados");
            }

            _unitOfWork.TagCategoyRepository.DeleteEntity(tagToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} tag fue eliminado con exito");

            return Unit.Value;

        }
    }
}

