using LaJuana.Application.Contracts.Persistence;
using LaJuana.Domain.Common;
using LaJuana.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System.Collections;

namespace LaJuana.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly LaJuanaDbContext _context;
        private IOrganizationRepository _organizationRepository;
        private IPeopleRepository _peopleRepository;
        private ITagRepository _tagRepository;
        private ITagCategoryRepository _tagCategoryRepository;
        private IAddressRepository _addressRepository;
        private IRelationshipRepository _relationshipRepository;
        private IRelationshipTypeRepository _relationshipTypeRepository;
        private IRelationshipDetailRepository _relationshipDetailRepository;
        public IOrganizationRepository OrganizationRepository => _organizationRepository ??= new OrganizationRepository(_context);
        public IPeopleRepository PeopleRepository => _peopleRepository ??= new PeopleRepository(_context);
        public ITagRepository TagRepository => _tagRepository ??= new TagRepository(_context);
        public ITagCategoryRepository TagCategoyRepository => _tagCategoryRepository ??= new TagCategoryRepository(_context);
        public IAddressRepository AddressRepository => _addressRepository ??= new AddressRepository(_context);
        public IRelationshipRepository RelationshipRepository => _relationshipRepository ??= new RelationshipRepository(_context);
        public IRelationshipTypeRepository RelationshipTypeRepository => _relationshipTypeRepository ??= new RelationshipTypeRepository(_context);
        public IRelationshipDetailRepository RelationshipDetailRepository => _relationshipDetailRepository ??= new RelationshipDetailRepository(_context);

        public UnitOfWork(LaJuanaDbContext context)
        {
            _context = context;
        }

        public LaJuanaDbContext LaJuanaDbContext => _context;

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }


    }
}
