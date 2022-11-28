using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using LaJuana.Application.Features.Relationship.Commands.CreateRelationship;
using Lucene.Net.Documents;
using LaJuana.Application.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Infrastructure.Repositories
{
    public class RelationshipRepository : RepositoryBase<Relationship>, IRelationshipRepository
    {
        private const string _indexPeopleLucene = "people";
        public RelationshipRepository(LaJuanaDbContext context) : base(context)
        {
        }
        private Document CreateDocumentRolParents(Guid personId,RelationshipType relationShip, bool IsNeutral){
            Document? doc= _context.lucene.FindDocByID(personId.ToString().ToLower(),_indexPeopleLucene);
            string rol=relationShip!.NeutralDescription;
            if(!IsNeutral){
                rol=doc!.Get("Gender").CompareTo("Masculino")==0
                                        ?
                                        relationShip!.MaleDescription
                                        :
                                        relationShip!.FemaleDescription;
            }
            doc!.RemoveField("Id");
            doc!.Add(new StringField("Id", personId.ToString(), Field.Store.YES));
            doc!.Add(new TextField("Rol", rol, Field.Store.YES));
            return doc;
        }
        private Document AddRelationShipToParent(Document parent, Document children,Guid relationID){
            //TODO cambie este por un objeto json.
            RelationShipByPersonVm d= new RelationShipByPersonVm();
            d.FirstName=children.Get("FirstName");
            d.PersonId=new Guid(children.Get("Id"));
            d.SecondName=children.Get("SecondName");
            d.LastName=children.Get("LastName");
            d.RelationshipTypeDescription=children.Get("Rol");
            d.RelationshipTypeDescriptionId=relationID;
            string relation = d.ToString(children.Get("Gender"));
            parent.Add(new StringField("RelationShips", relation, Field.Store.YES));
            return parent;
        }
        public async void AddEntityLucene(RelationshipCommand parent1,RelationshipCommand parent2){
            List<RelationshipType>? relations = await _context.RelationshipTypes!.Where(rs => rs.Id == parent1.RelationshipTypeID || rs.Id == parent2.RelationshipTypeID).ToListAsync();
            RelationshipType? relationShipParent1= relations.Find(rs => rs.Id == parent1.RelationshipTypeID);
            RelationshipType? relationShipParent2 =  relations.Find(rs => rs.Id == parent2.RelationshipTypeID);
            
            Document doc1 = CreateDocumentRolParents(
                                                parent1.PersonID,
                                                relationShipParent1,
                                                parent1.IsNeutral);
            Document doc2 = CreateDocumentRolParents(parent2.PersonID,
                                                relationShipParent2,
                                                parent2.IsNeutral);
            doc1=AddRelationShipToParent(doc1,doc2,relationShipParent2.Id);
            doc2=AddRelationShipToParent(doc2,doc1,relationShipParent1.Id);
            _context.lucene.UpdateDocumentAll(new List<Document>(){doc1,doc2},_indexPeopleLucene);
            
        }
        public  async Task<IEnumerable<PersonByRelationshipTypeVm>> GetPersonByRelationshipTypeFromLucene(String rol,Guid typeId)
        {
            
            List<PersonByRelationshipTypeVm> listPeople = _context.lucene.SearchTopDocs(
                new[] { "Rol" },
                rol,
                _indexPeopleLucene,
                (topDocs, searcher) =>
                {
                    List<PersonByRelationshipTypeVm> list = new List<PersonByRelationshipTypeVm>();
                    for (int i = 0; i < topDocs.TotalHits; i++)
                    {
                        PersonByRelationshipTypeVm people = new PersonByRelationshipTypeVm();
                        Document resultDoc = searcher.Doc(topDocs.ScoreDocs[i].Doc);
                        people.PersonId = new Guid(resultDoc.Get("Id"));
                        people.FirstName = resultDoc.Get("FirstName");
                        people.SecondName = resultDoc.Get("SecondName");
                        people.LastName = resultDoc.Get("LastName");
                        people.RelationshipTypeDescription = resultDoc.Get("Rol");
                        people.RelationshipTypeDescriptionId= typeId;
                        List<RelationShipByPersonVm> relationList = new List<RelationShipByPersonVm>();
                        foreach(string t in resultDoc.GetValues("RelationShips")){
                            RelationShipByPersonVm relation= new RelationShipByPersonVm();
                            relation.FromStringToObject(t);
                            relationList.Add(relation);
                        }
                        people.RelationShips=relationList;
                        list.Add(people);
                    }
                    return list;
                }
                );
            return listPeople;
        }
    }
}
