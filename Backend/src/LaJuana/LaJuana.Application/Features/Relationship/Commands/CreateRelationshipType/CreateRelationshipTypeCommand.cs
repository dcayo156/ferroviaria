using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.RelationshipType.Commands.CreateRelationshipType
{
    public class RelationshipTypeCommand
    {
        public Guid? RelationshipTypeRequiredID { get; set; }
        public string FemaleDescription { get; set; } = string.Empty;
        public string MaleDescription { get; set; } = string.Empty;
        public string NeutralDescription { get; set; } = string.Empty;
    }
    public class CreateRelationshipTypeCommand : IRequest<Guid>
    {
        public RelationshipTypeCommand[] Items { get; set; }
    }
}
