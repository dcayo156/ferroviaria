using LaJuana.Domain;
using LaJuana.Domain.Common;
using Microsoft.EntityFrameworkCore;
using LaJuana.Application.Contracts.Infrastructure;
using System.Net;
using System;

namespace LaJuana.Infrastructure.Persistence
{
    public class LaJuanaDbContext : DbContext
    {
        public ILuceneService lucene { get; set; }
        public LaJuanaDbContext(DbContextOptions<LaJuanaDbContext> options, ILuceneService luceneContext) : base(options)
        {
            lucene=luceneContext;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Program>().HasKey(p => p.Id);
        }
        public DbSet<Program>? Programs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
