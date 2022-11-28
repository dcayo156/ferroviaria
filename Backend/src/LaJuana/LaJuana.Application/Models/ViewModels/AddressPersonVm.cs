using LaJuana.Domain;
using System.Text.Json.Serialization;

namespace LaJuana.Application.Models.ViewModels
{
    public class AddressPersonVm
    {       
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; } = String.Empty;        
        public Guid PersonId { get; set; }
        public string FirstName { get { return Person?.FirstName; } }
        public string SecondName { get { return Person?.SecondName; } }
         public string LastName { get { return Person?.LastName; } }
        public string Name { get { return Person?.Name; } }
        [JsonIgnore]
        public PersonVm? Person { get; set; }
    }
}
