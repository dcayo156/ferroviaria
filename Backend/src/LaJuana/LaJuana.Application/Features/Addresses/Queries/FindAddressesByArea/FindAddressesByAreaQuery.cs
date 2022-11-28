using LaJuana.Application.Models.ViewModels;
using MediatR;
namespace LaJuana.Application.Features.Addresses.Queries
{    
    public class FindAddressesByAreaQuery : IRequest<List<AddressPersonVm>>
    {
        public double LongitudeFrom { get; set; }
        public double LatitudeFrom { get; set; }
        public double LongitudeTo { get; set; }        
        public double LatitudeTo { get; set; }
        
        
        public FindAddressesByAreaQuery(double longitudeFrom , double latitudeFrom, double  longitudeTo, double latitudeTo)
        {            
            LongitudeFrom = longitudeFrom;
            LatitudeFrom = latitudeFrom;
            LongitudeTo = longitudeTo;            
            LatitudeTo = latitudeTo;            
        }
    }
}
