using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(LaJuanaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Address>> FindAddressesByAreaAsync(double longitudeFrom, 
                                                                         double latitudeFrom,
                                                                         double longitudeTo,
                                                                         double latitudeTo) 
        {
            return await _context.Addresses!.
                Where(p => p.Longitude > Convert.ToDecimal(longitudeFrom)
                        && p.Longitude < Convert.ToDecimal(longitudeTo)
                        && p.Latitude < Convert.ToDecimal(latitudeFrom)
                        && p.Latitude > Convert.ToDecimal(latitudeTo))
                .Include(x => x.Person)
                .ThenInclude(i=>i.Tags)
                .ToListAsync();
        }
    }
}
