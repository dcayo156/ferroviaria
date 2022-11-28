using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using LaJuana.Domain;
namespace LaJuana.Application.Features.Tags.Queries.GetTagWithAddressesWithinMap
{
    public class GetTagWithAddressesWithinMapQueryHandler : IRequestHandler<GetTagWithAddressesWithinMapQuery, List<PersonAddress>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagWithAddressesWithinMapQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PersonAddress>> Handle(GetTagWithAddressesWithinMapQuery request, CancellationToken cancellationToken)
        {
            if (request.From.Longitud > request.To.Longitud)
                throw new Exception($"LongitudeFrom no puede ser mayor que LongitudeTo ");
            if (request.From.Latitud < request.To.Latitud)
                throw new Exception($"LatitudeFrom no puede ser menor que LatitudeTo ");

            if (request.fromLucene)
            {
                var tagList = await _unitOfWork.TagRepository.GetTagWithAddressesWithinMap(request.From,request.To,request.Categories!);
                return _mapper.Map<List<PersonAddress>>(tagList);
            }
            else
            {
                var addresList = await _unitOfWork.AddressRepository.FindAddressesByAreaAsync(Convert.ToDouble(request.From.Longitud),
                                                                                       Convert.ToDouble(request.From.Latitud),
                                                                                       Convert.ToDouble(request.To.Longitud),
                                                                                       Convert.ToDouble(request.To.Latitud));
                List<PersonAddress> result = new();
                foreach (Address address in addresList)
                {
                    string[] common;
                    if (request.Categories == null || request.Categories.Count() == 0)
                    {
                        var personData = await _unitOfWork.PeopleRepository.FindByIdAsync(address.PersonId);
                        result.Add(new PersonAddress()
                        {
                            Id = address.Id.ToString(),
                            Latitude = address.Latitude,
                            Longitude = address.Longitude,
                            PersonId = address.PersonId.ToString(),
                            Street = address.Street
                        });
                        continue;
                    }
                    Boolean t=true;
                    foreach(CategoryTags category in request.Categories){
                        if(t){
                            common = address.Person.Tags.Select(x => x.Id.ToString()).Intersect(category.TagIds).ToArray();
                            t=common.Any();
                        }
                    }
                    if (t)
                    {
                        var personData = await _unitOfWork.PeopleRepository.FindByIdAsync(address.PersonId);
                        result.Add(new PersonAddress()
                        {
                            Id = address.Id.ToString(),
                            Latitude = address.Latitude,
                            Longitude = address.Longitude,
                            PersonId = address.PersonId.ToString(),
                            Street = address.Street
                        });
                    }

                }
                return result;
            }
        }
    }
}

