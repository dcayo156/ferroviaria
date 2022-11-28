using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Models.ViewModels
{
    public class RelationshipGroupTypesVM
    {
        public Guid Id { get; set; }
        public Guid IdRelationType1 { get; set; }
        public Guid? IdRelationType2 { get; set; }
        public string RelationShipName1 { get; set; } = string.Empty;
        public string RelationShipName2 { get; set; } = string.Empty;
    }
}
