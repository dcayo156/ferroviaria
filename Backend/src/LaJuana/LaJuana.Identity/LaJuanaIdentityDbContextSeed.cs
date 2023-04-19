using LaJuana.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace LaJuana.Infrastructure.Persistence
{
    public class LaJuanaIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if ((await userManager.FindByNameAsync("admin@fosa.com")) == null)
            {
                var usuario = new ApplicationUser
                {
                    Nombre = "Admin",
                    Apellidos ="Admin",
                    UserName = "admin@fosa.com",
                    Email = "admin@fosa.com",
                    RefreshTokenExpiryTime = DateTime.Now,
                };

                await userManager.CreateAsync(usuario, "Admin123456$");
            }



            if ((await roleManager.FindByNameAsync("ADMIN")) == null)
            {
                var role = new IdentityRole
                {
                    Name = "ADMIN"
                };
                await roleManager.CreateAsync(role);
            }

            var user = (await userManager.FindByNameAsync("admin@fosa.com"));
            var roles = (await userManager.GetRolesAsync(user)).ToList();
            if (!roles.Contains("ADMIN"))
                await userManager.AddToRoleAsync(user, "ADMIN");
        }

    }
}
