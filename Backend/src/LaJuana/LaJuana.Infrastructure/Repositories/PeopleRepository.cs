using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.Diagnostics;
using LuceneDirectory = Lucene.Net.Store.Directory;

namespace LaJuana.Infrastructure.Repositories
{
    public class PeopleRepository : RepositoryBase<People>, IPeopleRepository
    {
        public const string _indexPeopleLucene = "people";
        public PeopleRepository(LaJuanaDbContext context) : base(context)
        {

        }

        public async Task<People> FindByIdAsync(Guid Id)
        {
            var people = await _context.Peoples!.Where(p => p.Id == Id)
                .Include(x => x.Addresses)
                .Include(x => x.CommunicationChannels)
                .Include(x => x.Tags).ThenInclude(x => x.TagCategory)

                .FirstOrDefaultAsync();

            return people;
        }

        public async Task<IEnumerable<People>> FindUserByNameAsync(string name)
        {
            return await _context.Peoples!.Where(p => p.FirstName.Contains(name) || p.SecondName.Contains(name) || p.LastName.Contains(name))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.SecondName)
                .ToListAsync();

        }
        public async Task<IEnumerable<People>> GetListPeople()
        {
            return await _context.Peoples!
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.SecondName)
                .ToListAsync();

        }
        public async Task<People> FindByNationalIdAsync(string nationalId)
        {
            var people = await _context.Peoples!.Where(p => p.NationalId.Equals(nationalId))
              .Include(x => x.Addresses)
              .Include(x => x.CommunicationChannels)
              .Include(x => x.Tags).ThenInclude(x => x.TagCategory)
              .FirstOrDefaultAsync();
              people.Tags.Where(t=>t.Id.ToString()=="");
            return people;
        }
        public async Task<PersonTag?> FindPersonTag(Guid personId, Guid tagId)
        {
            var personTag = await _context.PersonTags!.Where(p => p.PersonId == personId && p.TagId == tagId)
                .FirstOrDefaultAsync();
            return personTag;
        }
        public async Task<IEnumerable<T>> FindByTagIdAsync<T>(Guid tagId)
        where T : Person
        {
            var tags = await _context.Tags!.
            Where(p => p.Id.Equals(tagId))
            .Include(x => x.Persons)
            .FirstOrDefaultAsync();
            if (tags == null)
                throw new EntryPointNotFoundException();



            var peoples = tags?.Persons.Where(x => x is T).OfType<T>().ToArray();
            return peoples!;
        }

        public IEnumerable<People> OnLuceneFindPeopleByName(string name)
        {
            List<People> listPeople = _context.lucene.SearchTopDocs(
                new[] { "FirstName", "SecondName", "LastName", "Id" },
                name,
                _indexPeopleLucene,
                (topDocs, searcher) =>
                {
                    List<People> list = new List<People>();
                    for (int i = 0; i < topDocs.TotalHits; i++)
                    {
                        People people = new People();
                        Document resultDoc = searcher.Doc(topDocs.ScoreDocs[i].Doc);
                        people.Id = new Guid(resultDoc.Get("Id"));
                        people.FirstName = resultDoc.Get("FirstName");
                        people.SecondName = resultDoc.Get("SecondName");
                        people.LastName = resultDoc.Get("LastName");
                        people.Gender = resultDoc.Get("Gender") == "Masculino" ? Gender.Masculino : Gender.Femenino;
                        foreach(string t in resultDoc.GetValues("tags.Id")){
                            people.Tags.Add(new Tag(){
                                Id=new Guid(t)
                            });
                        }
                        list.Add(people);
                    }
                    return list;
                }
                );
            return listPeople.ToList();
        }

        public void AddEntityLucene(People peopleEntity)
        {
            Document doc = peopleEntity.ObjectFromLucene();
            _context.lucene.AddDocument(doc, _indexPeopleLucene);
        }

        public void AddEntitiesLucene(IEnumerable<People> peopleEntity)
        {
            List<Document> listPeople = new List<Document>();
            foreach (People people in peopleEntity)
            {
                Document doc = people.ObjectFromLucene();
                listPeople.Add(doc);
            }
            _context.lucene.AddDocumentAll(listPeople, _indexPeopleLucene);
        }

        public void UpdateEntityLucene(People peopleEntity)
        {
            Document doc = peopleEntity.ObjectFromLucene();
            _context.lucene.UpdateDocumentById(new Term("Id", peopleEntity.Id.ToString()), doc, _indexPeopleLucene);
        }

        public void DeleteEntityLucene(People peopleEntity)
        {
            _context.lucene.DeleteDocumentById(new Term("Id", peopleEntity.Id.ToString()), _indexPeopleLucene);
        }

        public void DeleteIndexLucene()
        {
            _context.lucene.DeleteDocumentAll(_indexPeopleLucene);
        }

        public async Task<IEnumerable<People>> FindUserAll()
        {
            return await _context.Peoples!
             .OrderBy(p => p.LastName)
             .ThenBy(p => p.FirstName)
             .ThenBy(p => p.SecondName)
             .Include(p => p.Tags)
             .Include(p => p.Addresses)
             .ToListAsync();
        }       
    }
}