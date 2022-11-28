using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Exceptions;
using LaJuana.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LaJuana.Application.Features.Tags.Commads.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteTagCommand> _logger;

        public DeleteTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteTagCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {

            // ask if exist tag
            var tagToDelete = await _unitOfWork.TagRepository.GetByIdAsync(request.Id);
            if (tagToDelete == null)
            {
                _logger.LogError($"{request.Id} tag no existe en el sistema");
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            // ask if exist name person
            var personTagExist = (await _unitOfWork.Repository<PersonTag>().GetAsync(m => m.TagId == request.Id)).FirstOrDefault();

            if (personTagExist != null)
            {
                _logger.LogInformation($"El tag con id: " + tagToDelete.Name + " se encuentra registrado con un Person Tag");
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            _unitOfWork.TagRepository.DeleteEntity(tagToDelete);

            var result=await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} tag fue eliminado con exito");
            if(result>0){
                _unitOfWork.TagRepository.DeleteEntityLucene(tagToDelete);
            }
            return Unit.Value;

        }
    }
}

