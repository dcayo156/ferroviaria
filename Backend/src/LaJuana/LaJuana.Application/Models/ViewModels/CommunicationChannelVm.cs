using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Models.ViewModels
{
    public class CommunicationChannelVm
    {
       public string Discriminator { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmailDescription { get; set; } = string.Empty;
        public string PhoneDescription { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;


         //public List<MailsVm>? Mail { get; set; }
         //public List<PhonesVm>? Phone { get; set; }
    }
}
