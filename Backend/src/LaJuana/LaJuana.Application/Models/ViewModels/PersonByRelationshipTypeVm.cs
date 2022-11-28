
namespace LaJuana.Application.Models.ViewModels
{
    public class PersonByRelationshipTypeVm
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RelationshipTypeDescription { get; set; } = string.Empty;
        public Guid RelationshipTypeDescriptionId { get; set; }
        public IReadOnlyList<RelationShipByPersonVm>? RelationShips { get; set; }
    }
}
