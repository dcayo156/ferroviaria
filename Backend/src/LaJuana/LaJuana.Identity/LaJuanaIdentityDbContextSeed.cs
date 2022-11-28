using LaJuana.Domain;
using LaJuana.Identity;
using LaJuana.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Infrastructure.Persistence
{
    public class LaJuanaIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if ((await userManager.FindByNameAsync("admin@k27.com.ar")) == null)
            {
                var usuario = new ApplicationUser
                {
                    Nombre = "Admin",
                    Apellidos ="Admin",
                    UserName = "admin@k27.com.ar",
                    Email = "admin@k27.com.ar",
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

            var user = (await userManager.FindByNameAsync("admin@k27.com.ar"));
            var roles = (await userManager.GetRolesAsync(user)).ToList();
            if (!roles.Contains("ADMIN"))
                await userManager.AddToRoleAsync(user, "ADMIN");
        }

    }
}
