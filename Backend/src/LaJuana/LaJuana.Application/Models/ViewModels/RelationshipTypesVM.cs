using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Models.ViewModels
{
    public class RelationshipTypesVM
    {
        public Guid Id { get; set; }
        public Guid? RelationshipTypeRequiredID { get; set; }
        public string FemaleDescription { get; set; } = string.Empty;
        public string MaleDescription { get; set; } = string.Empty;
        public string NeutralDescription { get; set; } = string.Empty;
        public string RelationshipName
        {
            get
            {
                return $"{MaleDescription} / {FemaleDescription} / {NeutralDescription}";
            }
        }
    }
}
