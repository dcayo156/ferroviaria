using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Categories.Commands.DeleteCategories
{
    public class DeleteCategoriesCommandHandler : IRequestHandler<DeleteCategoriesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCategoriesCommandHandler> _logger;

        public DeleteCategoriesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteCategoriesCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCategoriesCommand request, CancellationToken cancellationToken)
        {
            var CategorysToDelete = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (CategorysToDelete == null)
            {
                _logger.LogError($"{request.Id} Category no existe en el sistema");
                throw new NotFoundException(nameof(Category), request.Id);
            }
            _unitOfWork.Repository<Category>().DeleteEntity(CategorysToDelete);

            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} Categorys fue eliminado con exito");

            return Unit.Value;
        }
    }
}