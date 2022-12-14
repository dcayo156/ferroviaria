using LaJuana.Application.Contracts.Identity;
using LaJuana.Application.Models.Identity;
using LaJuana.Identity.Models;
using LaJuana.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LaJuana.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            //var DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "LaJuanaSecurity.db");
            services.AddDbContext<LaJuanaIdentityDbContext>(options =>
                                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
                                //b => b.MigrationsAssembly(typeof(LaJuanaIdentityDbContext).Assembly.FullName)));
                                //options.UseSqlite($"Data Source={DbPath}"));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LaJuanaIdentityDbContext>().AddDefaultTokenProviders();


            services.AddTransient<IAuthService, AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew   = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };

            });


            return services;

        }
    }
}
