using LaJuana.Domain;
using System;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Application.Features.Tags.Queries.GetTagWithAddressesWithinMap;
namespace LaJuana.Application.Contracts.Persistence
{
	public interface ITagRepository : IAsyncRepository<Tag>
	{
		Task<Tag> FindTagByIdAsync(Guid Id);
		Task<IEnumerable<Tag>> FindTagByCategoryIdAsync(Guid Id);
		Task<IEnumerable<Tag>> FindTagByNameAsync(string name);
		Task<IEnumerable<Tag>> GetTagsListAsync();
		Task<IEnumerable<PersonAddress>> GetTagWithAddressesWithinMap(CardinalPoint from,CardinalPoint to,List<CategoryTags> categories);
		Task<IEnumerable<TagsWithAddressCountOfPersonVm>> GetTagsWithAddressCountOfPerson();
		void AddEntityLucene(Tag tagEntity,TagCategory tagCategoryEntity);
		void EditEntityLucene(Tag EditEntityLucene,TagCategory tagCategoryEntity);
		void DeleteEntityLucene(Tag DeleteEntityLucene);
		void AddPersonToTagInLucene(People peopleEntity,Tag tagEntity);	
        void AddEntitiesLucene(IEnumerable<Tag> tagEntities);
        void DeleteIndexLucene();
	}
}

