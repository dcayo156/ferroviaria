using LaJuana.Application.Features.Addresses.Queries;
using LaJuana.Application.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaJuana.API.Controllers
{
    [Route("v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("FindAddressesByArea/{longitudFrom}/{latitudFrom}/{longitudTo}/{latitudTo}", Name = "FindAddressesByArea")]        
        [ProducesResponseType(typeof(IEnumerable<AddressPersonVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AddressPersonVm>>> FindAddressesByArea (double longitudFrom,
                                                                                           double latitudFrom,
                                                                                           double longitudTo,
                                                                                           double latitudTo)
        {
              var query = new FindAddressesByAreaQuery(longitudFrom, latitudFrom, longitudTo, latitudTo);            
            var addresses = await _mediator.Send(query);
            return Ok(addresses);
        }
    }
}
