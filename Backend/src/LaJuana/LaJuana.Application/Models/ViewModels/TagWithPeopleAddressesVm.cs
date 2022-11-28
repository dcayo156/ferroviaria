
using System.Text.Json;
using LaJuana.Domain;
namespace LaJuana.Application.Models.ViewModels
{
    public class PersonAddress
    {
        public string Id {get;set;}= string.Empty;
        public decimal Latitude {get;set;}
        public decimal Longitude{get;set;}
        public string Street {get;set;}= string.Empty;
        public string PersonId{get;set;}= string.Empty;
        public void FromStringToObject(string t){
            Address? a= JsonSerializer.Deserialize<Address>(t);
            if(a!=null){
                this.Id=a.Id.ToString();
                this.Latitude=a.Latitude;
                this.Longitude=a.Longitude;
                this.Street=a.Street;
                this.PersonId=a.PersonId.ToString();
            }
        }
    }
    public class TagWithPeopleAddressesVm
    {
        public string Name { get; set; } = string.Empty;
        public string Id {get;set;} = string.Empty;
        public string CategoryId {get;set;}= string.Empty;
        public string CategoryName {get;set;}= string.Empty;
        public List<PersonAddress> Addresses { get; set; } = new List<PersonAddress>();
        
    }
}
