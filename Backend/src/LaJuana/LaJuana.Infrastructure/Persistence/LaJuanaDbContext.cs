using LaJuana.Domain;
using LaJuana.Domain.Common;
using Microsoft.EntityFrameworkCore;
using LaJuana.Application.Contracts.Infrastructure;
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

            modelBuilder.Entity<Address>().Property(a => a.Latitude).HasPrecision(18, 9);

            modelBuilder.Entity<Address>().Property(a => a.Longitude).HasPrecision(18, 9);

            modelBuilder.Entity<Person>()
                .HasMany(m => m.Addresses)
                .WithOne(m => m.Person)
                .HasForeignKey(m => m.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(m => m.CommunicationChannels)
                .WithOne(m => m.Person)
                .HasForeignKey(m => m.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Persons)
                .UsingEntity<PersonTag>(
                    pt => pt.HasKey(e => new { e.TagId, e.PersonId }));

            modelBuilder.Entity<Person>()
                .HasMany(m => m.RelationshipDetails)
                .WithOne(m => m.Person)
                .HasForeignKey(m => m.PersonID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonTag>().HasKey(p => p.Id);

            modelBuilder.Entity<Relationship>()
                .HasMany(m => m.RelationshipDetails)
                .WithOne(m => m.Relationship)
                .HasForeignKey(m => m.RelationshipID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RelationshipType>()
              .HasMany(m => m.RelationshipDetails)
              .WithOne(m => m.RelationshipType)
              .HasForeignKey(m => m.RelationshipTypeID)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationshipType>()
            .HasOne(b => b.RelationshipTypeRequired)
            .WithOne()
            .HasForeignKey<RelationshipType>(b => b.RelationshipTypeRequiredID);

            modelBuilder.Entity<TagCategory>()
                .HasMany(m => m.Tags)
                .WithOne(m => m.TagCategory)
                .HasForeignKey(m => m.TagCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
        public DbSet<RelationshipType>? RelationshipTypes { get; set; }
        public DbSet<RelationshipDetail>? RelationshipDetails { get; set; }
        public DbSet<Relationship>? Relationships { get; set; }
        public DbSet<CommunicationChannel>? CommunicationChannels { get; set; }
        public DbSet<Mail>? Mails { get; set; }
        public DbSet<Phone>? Phones { get; set; }
        public DbSet<Person>? Persons { get; set; }

        public DbSet<People>? Peoples { get; set; }

        public DbSet<Organization>? Organizations { get; set; }

        public DbSet<Address>? Addresses { get; set; }

        public DbSet<Tag>? Tags { get; set; }

        public DbSet<PersonTag>? PersonTags { get; set; }
        public DbSet<TagCategory>? TagCategories { get; set; }
        
        
    }
}
