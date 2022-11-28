using MediatR;


namespace LaJuana.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = string.Empty;

        public int City { get; set; }

        public int State { get; set; }

        public int Country { get; set; }

        public string ZipCode { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Description { get; set; } = string.Empty;

        public Guid PersonId { get; set; }

    }
}
