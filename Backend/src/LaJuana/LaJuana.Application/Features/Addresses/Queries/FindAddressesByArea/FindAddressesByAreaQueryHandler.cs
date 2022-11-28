using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;


namespace LaJuana.Application.Features.Addresses.Queries.FindAddressesByArea
{
    public class FindAddressesByAreaQueryHandler : IRequestHandler<FindAddressesByAreaQuery, List<AddressPersonVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public FindAddressesByAreaQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AddressPersonVm>> Handle(FindAddressesByAreaQuery request, CancellationToken cancellationToken)
        {
            if (request.LongitudeFrom > request.LongitudeTo) 
            {
                throw new Exception($"LongitudeFrom no puede ser mayor que LongitudeTo ");
            }
            if (request.LatitudeFrom < request.LatitudeTo)
            {
                throw new Exception($"LatitudeFrom no puede ser menor que LatitudeTo ");
            }
            var addresList = await _unitOfWork.AddressRepository.FindAddressesByAreaAsync(request.LongitudeFrom,
                                                                                        request.LatitudeFrom,
                                                                                        request.LongitudeTo,
                                                                                        request.LatitudeTo);

            

            return _mapper.Map<List<AddressPersonVm>>(addresList);
        }
    }
}
