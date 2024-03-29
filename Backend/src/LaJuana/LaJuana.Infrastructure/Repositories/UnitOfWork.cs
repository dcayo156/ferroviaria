﻿using LaJuana.Application.Contracts.Persistence;
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
        private IProgramsRepository _programsRepository;
        public IProgramsRepository ProgramRepository => _programsRepository ??= new ProgramsRepository(_context);

        private ICategoriesRepository _categoriesRepository;
        public ICategoriesRepository CategoryRepository => _categoriesRepository ??= new CategoriesRepository(_context);
        private IDocumentsRepository _DocumentsRepository;
        public IDocumentsRepository DocumentRepository => _DocumentsRepository ??= new DocumentsRepository(_context);

        private IInspectionTrainsRepository _InspectionTrainsRepository;
        public IInspectionTrainsRepository InspectionTrainsRepository => _InspectionTrainsRepository ??= new InspectionTrainsRepository(_context);
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
