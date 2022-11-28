using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Models.ViewModels
{
    public class PhoneVm
    {
        public Guid Id { get; set; }
        public string PhoneDescription { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
