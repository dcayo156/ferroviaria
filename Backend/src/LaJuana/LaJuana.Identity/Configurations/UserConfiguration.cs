using LaJuana.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaJuana.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
                        Email = "admin@locahost.com",
                        NormalizedEmail = "admin@locahost.com",
                        Nombre = "Pepe",
                        Apellidos = "Argento",
                        UserName = "pepeargento",
                        NormalizedUserName = "pepeargento",
                        PasswordHash = hasher.HashPassword(null, "pepe-007"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "294d249b-9b57-48c1-9689-11a91abb6447",
                        Email = "juanperez@locahost.com",
                        NormalizedEmail = "lamoni@locahost.com",
                        Nombre = "Moni",
                        Apellidos = "Argento",
                        UserName = "moniargento",
                        NormalizedUserName = "moniargento",
                        PasswordHash = hasher.HashPassword(null, "moni-007"),
                        EmailConfirmed = true,
                    }

                );


        }
    }
}
