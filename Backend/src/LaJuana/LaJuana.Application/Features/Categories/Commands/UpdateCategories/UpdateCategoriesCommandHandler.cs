using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Categories.Commands.UpdateCategories
{
    public class UpdateCategoriesCommandHandler : IRequestHandler<UpdateCategoriesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoriesCommand> _logger; 
        public UpdateCategoriesCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateCategoriesCommand> logger)      
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;          
        }      

        public async Task<Unit> Handle(UpdateCategoriesCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new Exception("El objeto es null"); }           

            var categorysToUpdate = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (categorysToUpdate == null)
            {
                _logger.LogError($"No se encontro el Category id {request.Id}");
                throw new NotFoundException(nameof(Category), request.Id);
            }
            _mapper.Map(request, categorysToUpdate, typeof(UpdateCategoriesCommand), typeof(Category));

            _unitOfWork.Repository<Category>().UpdateEntity(categorysToUpdate);
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el Categorys {request.Id}");
            return Unit.Value;
        }
    }
}
