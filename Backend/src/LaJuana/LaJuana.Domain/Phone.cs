using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Domain
{
    public class Phone : CommunicationChannel
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneDescription { get; set; } = string.Empty;
    }
}

