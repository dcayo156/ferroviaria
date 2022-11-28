using LaJuana.Domain;
using System;


namespace LaJuana.Application.Models.ViewModels
{
   

    public class PeopleFullVm
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public PronounPreference? PronounPreference { get; set; }
        public List<AddressVm>? Addresses { get; set; }      
        public List<CommunicationChannelVm>? CommunicationChannels { get; set; }        
        public List<TagVm>? Tags { get; set; }
        

    }
}
