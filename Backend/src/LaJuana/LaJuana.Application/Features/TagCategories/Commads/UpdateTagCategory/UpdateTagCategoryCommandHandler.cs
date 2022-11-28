using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.TagCategories.Commads.UpdateTagCategory
{
    public class UpdateTagCategoryCommandHandler : IRequestHandler<UpdateTagCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTagCategoryCommandHandler> _logger;

        public UpdateTagCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateTagCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateTagCategoryCommand request, CancellationToken cancellationToken)
        {

            //ask if exist tag
            var tagToUpdate = await _unitOfWork.TagCategoyRepository.GetByIdAsync(request.Id);

            if (tagToUpdate == null)
            {
                _logger.LogError($"No se encontro el tag category con el id {request.Id}");
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            var tagExist = (await _unitOfWork.Repository<Tag>().GetAsync(m => m.Name == request.Description && m.Id != request.Id)).FirstOrDefault();

            if (tagExist != null)
            {
                _logger.LogInformation($"El tag Category con el nombre: " + request.Description + " se encuentra registrado");
                throw new NotFoundException(nameof(Tag), request.Id);
            }


            _mapper.Map(request, tagToUpdate, typeof(UpdateTagCategoryCommand), typeof(UpdateTagCategoryCommand));
            _unitOfWork.TagCategoyRepository.UpdateEntity(tagToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el tag {request.Id}");

            return Unit.Value;


        }
    }
}

