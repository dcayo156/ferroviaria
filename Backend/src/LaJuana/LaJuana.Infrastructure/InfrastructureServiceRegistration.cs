using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models;
using LaJuana.Infrastructure.Email;
using LaJuana.Infrastructure.Persistence;
using LaJuana.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaJuana.Infrastructure.Persistence;
namespace LaJuana.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            var DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "LaJuana.db");
            services.AddDbContext<LaJuanaDbContext>(options =>
                //options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
                options.UseSqlite($"Data Source={DbPath}")
            );
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<LuceneSettings>(c => configuration.GetSection("LuceneSettings"));
            services.AddTransient<ILuceneService, LaJuanaLuceneContext>();
            return services;
        }

    }
}
