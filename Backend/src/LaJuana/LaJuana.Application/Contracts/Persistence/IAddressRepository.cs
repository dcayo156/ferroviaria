
using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IAddressRepository : IAsyncRepository<Address>
    {
        Task<IEnumerable<Address>> FindAddressesByAreaAsync(double longitudeFrom,
                                                            double latitudeFrom, 
                                                            double longitudeTo, 
                                                            double latitudeTo);
    }
}
