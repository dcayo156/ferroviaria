using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Domain
{
    public class Mail 
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmailDescription { get; set; } = string.Empty;
    }
}
