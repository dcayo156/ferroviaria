﻿using LaJuana.Domain.Common;

namespace LaJuana.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {   
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
    }
}
