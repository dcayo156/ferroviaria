using LaJuana.Domain;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IPeopleRepository : IAsyncRepository<People>
    {

        Task<IEnumerable<People>> FindUserByNameAsync(string name);
        Task<IEnumerable<People>> FindUserAll();
        IEnumerable<People> OnLuceneFindPeopleByName(string name);
        Task<IEnumerable<People>> GetListPeople();

        Task<People> FindByIdAsync(Guid Id);
        Task<People> FindByNationalIdAsync(string nationalId);
        Task<PersonTag?> FindPersonTag(Guid personId, Guid TagId);
        Task<IEnumerable<T>> FindByTagIdAsync<T>(Guid tagId) where T : Person;
        void AddEntityLucene(People peopleEntity);
        void AddEntitiesLucene(IEnumerable<People> peopleEntity);
        void UpdateEntityLucene(People peopleEntity);
        void DeleteEntityLucene(People peopleEntity);
        void DeleteIndexLucene();
    }
}
