using LaJuana.Domain.Common;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IPeopleRepository PeopleRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }
        ITagRepository TagRepository { get; }
        ITagCategoryRepository TagCategoyRepository { get; }
        IRelationshipRepository RelationshipRepository { get; }
        IRelationshipTypeRepository RelationshipTypeRepository { get; }
        IRelationshipDetailRepository RelationshipDetailRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
        IAddressRepository AddressRepository { get; }
    }
}
