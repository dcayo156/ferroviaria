using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Tags.Commads.UpdateTag
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTagCommandHandler> _logger;

        public UpdateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateTagCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {

            //ask if exist tag
            var tagToUpdate = await _unitOfWork.TagRepository.GetByIdAsync(request.Id);

            if (tagToUpdate == null)
            {
                _logger.LogError($"No se encontro el tag con el id {request.Id}");
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            // ask if exist name tag
            var tagExist = (await _unitOfWork.Repository<Tag>().GetAsync(m => m.Name == request.Name && m.Id != request.Id)).FirstOrDefault();

            if (tagExist != null)
            {
                _logger.LogInformation($"El tag con el nombre: " + request.Name + " se encuentra registrado");
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            // ask if exist name person
            var personTagExist = (await _unitOfWork.Repository<PersonTag>().GetAsync(m => m.TagId == request.Id)).FirstOrDefault();

            if (personTagExist != null)
            {
                _logger.LogInformation($"El tag con id: " + request.Name + " se encuentra registrado con un Person Tag");
                throw new NotFoundException(nameof(Tag), request.Id);
            }
            var tagcategory = (await _unitOfWork.Repository<TagCategory>().GetAsync(tc=>tc.Id==tagToUpdate.TagCategoryId)).FirstOrDefault();
            if (tagcategory == null)
            {
                throw new Exception($"El tag Category con ID: " + request.TagCategoryId.ToString() + " no se encuentra registrado");
            }
            _mapper.Map(request, tagToUpdate, typeof(UpdateTagCommand), typeof(People));
            _unitOfWork.TagRepository.UpdateEntity(tagToUpdate);

            var result=await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el tag {request.Id}");
            
            if(result>0){
                _unitOfWork.TagRepository.EditEntityLucene(tagToUpdate,tagcategory);
            }

            return Unit.Value;


        }
    }
}

