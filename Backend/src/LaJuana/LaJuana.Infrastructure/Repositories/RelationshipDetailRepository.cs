using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace LaJuana.Infrastructure.Repositories
{
    public class RelationshipDetailRepository : RepositoryBase<RelationshipDetail>, IRelationshipDetailRepository
    {
        public RelationshipDetailRepository(LaJuanaDbContext context) : base(context)
        {
        }
        private string GetRelationshipDescription(Gender? gender, RelationshipType relationshipType)
        {
            switch (gender)
            {
                case Gender.Femenino:
                    return relationshipType.FemaleDescription;
                case Gender.Masculino:
                    return relationshipType.MaleDescription;
                case Gender.Personalizado:
                    return relationshipType.NeutralDescription;
                default:
                    throw new ArgumentException();
            }
        }
        private List<Expression<Func<RelationshipDetail, object>>> GetIncludesForRelationShipDetail()
        {
            var includes = new List<Expression<Func<RelationshipDetail, object>>>();
            includes.Add(x => x.Person!);
            includes.Add(x => x.RelationshipType!);
            includes.Add(x => x.Relationship!);
            return includes;
        }
        private async Task<IReadOnlyList<RelationShipByPersonVm>> AddRelations(IReadOnlyList<RelationshipDetail> relationshiptDetailByRelationType, Guid personID)
        {
            var relationsResult = new List<RelationShipByPersonVm>();
            var relations = relationshiptDetailByRelationType.Where(x => x.PersonID == personID)?.ToArray();
            foreach (var relation in relations)
            {
                var relatedPerson = await GetAsync(r => r.RelationshipID == relation.RelationshipID && r.PersonID != relation.PersonID, includes: GetIncludesForRelationShipDetail());
                People parentPeople = relatedPerson.FirstOrDefault()!.Person as People;
                relationsResult.Add(new RelationShipByPersonVm()
                {
                    PersonId = relatedPerson.FirstOrDefault()!.PersonID,
                    FirstName = parentPeople.FirstName,
                    SecondName = parentPeople.SecondName,
                    LastName = parentPeople.LastName,
                    RelationshipTypeDescription = GetRelationshipDescription(parentPeople.Gender, relatedPerson.FirstOrDefault().RelationshipType),
                    RelationshipTypeDescriptionId = relatedPerson.FirstOrDefault()!.RelationshipTypeID,
                });
            }
            return relationsResult.ToArray();
        }
        public  async Task<IEnumerable<PersonByRelationshipTypeVm>> GetPersonByRelationshipType(Guid relationshipTypeId, Gender gender)
        {
            var relationshiptDetailByRelationType = await GetAsync(r => (r.IsNeutral || ((People)r.Person).Gender == gender)
                                                                   && r.RelationshipTypeID == relationshipTypeId,
                                                                   includes: GetIncludesForRelationShipDetail());
            var result = new List<PersonByRelationshipTypeVm>();
            Guid[] personIDs = relationshiptDetailByRelationType.Select(x => x.PersonID).Distinct().ToArray();

            foreach (Guid personID in personIDs)
            {
                var person = relationshiptDetailByRelationType.Where(x => x.PersonID == personID)?.FirstOrDefault();
                PersonByRelationshipTypeVm personResult = new();
                People people = person.Person as People;
                personResult.PersonId = person.PersonID;
                personResult.FirstName = people.FirstName;
                personResult.SecondName = people.SecondName;
                personResult.LastName = people.LastName;
                personResult.RelationshipTypeDescription = GetRelationshipDescription(people.Gender, person.RelationshipType);
                personResult.RelationshipTypeDescriptionId = person.RelationshipTypeID;
                personResult.RelationShips = await AddRelations(relationshiptDetailByRelationType, person.PersonID);
                result.Add(personResult);
            }
            return result;
        }
        public async Task<IEnumerable<RelationShipByPersonVm>> GetRelationShipsByPersonId(Guid personId)
        {
            var includes = new List<Expression<Func<RelationshipDetail, object>>>();
            includes.Add(x => x.Person!);
            includes.Add(x => x.RelationshipType!);
            includes.Add(x => x.Relationship!);

            var relationshiptDetailByPerson = await   GetAsync(r => r.PersonID == personId, x => x.OrderBy(p => p.PersonID), includes);
            var result = new List<RelationShipByPersonVm>();
            foreach (var detail in relationshiptDetailByPerson)
            {
                var relation = await GetAsync(r => r.RelationshipID == detail.RelationshipID &&
                                            r.Id != detail.Id, x => x.OrderBy(p => p.PersonID), includes);
                if (relation.Any())
                {
                    var person = relation.First().Person as People;
                    string description = "";
                    if (relation.First().IsNeutral)
                    {
                        description = relation.First().RelationshipType!.NeutralDescription;
                    }
                    else
                    {
                        description = person!.Gender == Domain.Gender.Femenino ?
                                        relation.First().RelationshipType!.FemaleDescription
                                        :
                                        relation.First().RelationshipType!.MaleDescription;
                    }
                    result.Add(new RelationShipByPersonVm()
                    {
                        Id = detail.RelationshipID,
                        PersonId = relation.First().PersonID,
                        FirstName = person!.FirstName,
                        LastName = person!.LastName,
                        SecondName = person!.SecondName,
                        RelationshipTypeDescription = description,
                        RelationshipTypeDescriptionId = relation.First().RelationshipTypeID
                    });
                }
            }
            return result;

        }
    }
}
