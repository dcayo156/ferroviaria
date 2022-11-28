using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Models.ViewModels
{
    public class AddressVm
    {
        public Guid Id { get; set; } 
        public string Street { get; set; } = String.Empty;
        public int City { get; set; } 
        public int State { get; set; }
        public int Country { get; set; } 
        public string ZipCode { get; set; } = String.Empty;
        public double Latitude { get; set; } 
        public double Longitude { get; set; }
        public string Description { get; set; } = String.Empty;
        public Guid PersonId { get; set; }

    }
}
