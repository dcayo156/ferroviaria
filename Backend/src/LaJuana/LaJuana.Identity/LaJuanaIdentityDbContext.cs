using LaJuana.Identity.Configurations;
using LaJuana.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaJuana.Identity
{
    public class LaJuanaIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public LaJuanaIdentityDbContext(DbContextOptions<LaJuanaIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
