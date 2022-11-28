using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using LaJuana.Application.Features.Tags.Queries.GetTagWithAddressesWithinMap;

namespace LaJuana.Infrastructure.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        private const string _indexTagLucene = "tags";
        public TagRepository(LaJuanaDbContext context) : base(context)
        {
        }


        public async Task<Tag> FindTagByIdAsync(Guid Id)
        {
            var tag = await _context.Tags!.Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            return tag;
        }

        public async Task<IEnumerable<Tag>> FindTagByNameAsync(string name)
        {
            return await _context.Tags!.Where(p => p.Name.Contains(name))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> FindTagByCategoryIdAsync(Guid Id)
        {
            return await _context.Tags!.Where(x => x.TagCategoryId == Id)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsListAsync()
        {
            return await _context.Tags!
                .OrderBy(p => p.Name)
                .Include(t => t.TagCategory)                
                .ToListAsync();
        }
        public async Task<IEnumerable<TagsWithAddressCountOfPersonVm>> GetTagsWithAddressCountOfPerson(){
            List<TagsWithAddressCountOfPersonVm> LisTagWithPeopleAddress = _context.lucene.AllDoc(
                _indexTagLucene,
                (topDocs, searcher) =>
                {
                    List<TagsWithAddressCountOfPersonVm> list = new List<TagsWithAddressCountOfPersonVm>();
                    for (int i = 0; i < topDocs.TotalHits; i++)
                    {
                        TagsWithAddressCountOfPersonVm tag = new TagsWithAddressCountOfPersonVm();
                        Document resultDoc = searcher.Doc(topDocs.ScoreDocs[i].Doc);
                        tag.Id=resultDoc.Get("Id");
                        tag.CategoryId=resultDoc.Get("CategoryId");
                        tag.CategoryName=resultDoc.Get("CategoryName");
                        tag.Name=resultDoc.Get("Name");
                        tag.NumberOfPeople=resultDoc.GetValues("PersonAddress").Length;
                        list.Add(tag);
                    }
                    return list;
                }
                );
            return LisTagWithPeopleAddress;
        }
        protected Boolean InBound(CardinalPoint from,CardinalPoint to, CardinalPoint inbound ){
            return  inbound.Longitud > from.Longitud
                    && inbound.Longitud < to.Longitud
                    && inbound.Latitud < from.Latitud
                    && inbound.Latitud > to.Latitud;
        }
        public async Task<IEnumerable<PersonAddress>> GetTagWithAddressesWithinMap(CardinalPoint from, CardinalPoint to, List<CategoryTags> categories)
        {
            List<Query> queries= new List<Query>();
            List<Term> term=new List<Term>();
            List<PersonAddress> LisTagWithPeopleAddress = new List<PersonAddress>();
            if(categories!=null && categories.Count > 0){
                string queryString="";
                int countCategory=0;
                foreach(CategoryTags ct in categories){
                    if(ct.TagIds.Count>0){
                        List<string> tags=ct.TagIds!;
                        queryString=$"{queryString}(";
                        int countTag=0; 
                        foreach(string t in tags){
                            if(countTag++==0){
                                queryString=$"{queryString} {t}";
                            }else{
                                queryString=$"{queryString} OR {t}";
                            }
                        }
                        queryString=$"{queryString})";
                    }
                    if(++countCategory < categories.Count)
                        queryString=$"{queryString} AND ";
                }
                LisTagWithPeopleAddress = _context.lucene.SearchTopDocs(
                new[] { "Tags" },
                queryString,
                PeopleRepository._indexPeopleLucene,
                (topDocs, searcher) =>
                {
                    List<PersonAddress> list = new List<PersonAddress>();
                    for (int i = 0; i < topDocs.TotalHits; i++)
                    {
                        Document resultDoc = searcher.Doc(topDocs.ScoreDocs[i].Doc);
                        List<PersonAddress> peopleAddresses = new List<PersonAddress>();
                        foreach(string t in resultDoc.GetValues("Address")){
                            PersonAddress personAddresses= new PersonAddress();
                            personAddresses.FromStringToObject(t);
                            if(InBound(from,to,new CardinalPoint(){
                                Latitud=personAddresses.Latitude,
                                Longitud=personAddresses.Longitude
                            })){ 
                                list.Add(personAddresses);
                            }
                        }                        
                    }
                    return list;
                }
                );
            }
            else{
                LisTagWithPeopleAddress =_context.lucene.AllDoc(
                PeopleRepository._indexPeopleLucene,
                (topDocs, searcher) =>
                {
                    List<PersonAddress> list = new List<PersonAddress>();
                    for (int i = 0; i < topDocs.TotalHits; i++)
                    {
                        Document resultDoc = searcher.Doc(topDocs.ScoreDocs[i].Doc);
                        List<PersonAddress> peopleAddresses = new List<PersonAddress>();
                        foreach(string t in resultDoc.GetValues("Address")){
                            PersonAddress personAddresses= new PersonAddress();
                            personAddresses.FromStringToObject(t);
                            if(InBound(from,to,new CardinalPoint(){
                                Latitud=personAddresses.Latitude,
                                Longitud=personAddresses.Longitude
                            })){ 
                                list.Add(personAddresses);
                            }
                        }                        
                    }
                    return list;
                }
                );
            }
            return LisTagWithPeopleAddress;
        }
        public void AddEntityLucene(Tag tagEntity,TagCategory tagCategoryEntity)
        {
            Document doc = tagEntity.ObjectFromLucene();
            _context.lucene.AddDocument(doc, _indexTagLucene);
        }
        public void AddPersonToTagInLucene(People peopleEntity,Tag tagEntity){
            foreach (Address address in peopleEntity.Addresses)
            {
                Document? tagDoc= _context.lucene.FindDocByID(tagEntity.Id.ToString().ToLower(),_indexTagLucene);
                tagDoc!.RemoveField("Id");
                tagDoc!.Add(new StringField("Id", tagEntity.Id.ToString(), Field.Store.YES));
                tagDoc!.Add(new TextField("PersonAddress", address.ToString(), Field.Store.YES));
                _context.lucene.UpdateDocumentById(new Term("Id", tagEntity.Id.ToString()), tagDoc, _indexTagLucene);    
            }
        }

        public void EditEntityLucene(Tag EditEntityLucene,TagCategory tagCategoryEntity)
        {
            //Document tagDoc = EditEntityLucene.ObjectFromLucene();
            Document? tagDoc= _context.lucene.FindDocByID(EditEntityLucene.Id.ToString().ToLower(),_indexTagLucene);
            tagDoc!.RemoveField("Id");
            tagDoc!.RemoveField("CategoryId");
            tagDoc!.RemoveField("CategoryName");
            tagDoc!.RemoveField("Name");
            tagDoc!.Add(new StringField("Id", EditEntityLucene.Id.ToString(), Field.Store.YES));
            tagDoc!.Add(new StringField("CategoryId", EditEntityLucene.TagCategoryId.ToString(), Field.Store.YES));
            tagDoc!.Add(new StringField("CategoryName", tagCategoryEntity.Description, Field.Store.YES));
            tagDoc!.Add(new TextField("Name", EditEntityLucene.Name, Field.Store.YES));
            _context.lucene.UpdateDocumentById(new Term("Id", EditEntityLucene.Id.ToString()), tagDoc, _indexTagLucene);    
        }
        public void DeleteEntityLucene(Tag DeleteEntityLucene)
        {
            _context.lucene.DeleteDocumentById(new Term("Id", DeleteEntityLucene.Id.ToString()), _indexTagLucene);
        }

        

        public void AddEntitiesLucene(IEnumerable<Tag> tagEntities)
        {
            List<Document> listTags = new List<Document>();
            foreach (Tag tag in tagEntities)
            {
                Document doc = tag.ObjectFromLucene();
                listTags.Add(doc);
            }
            _context.lucene.AddDocumentAll(listTags, _indexTagLucene);
        }

        public void DeleteIndexLucene()
        {
            _context.lucene.DeleteDocumentAll(_indexTagLucene);
        }
    }
}

