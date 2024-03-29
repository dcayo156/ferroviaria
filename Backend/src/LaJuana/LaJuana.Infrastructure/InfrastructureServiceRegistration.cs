﻿using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models;
using LaJuana.Infrastructure.Apose;
using LaJuana.Infrastructure.Directories;
using LaJuana.Infrastructure.Email;
using LaJuana.Infrastructure.MineType;
using LaJuana.Infrastructure.Persistence;
using LaJuana.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LaJuana.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //var DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "LaJuana.db");
            services.AddDbContext<LaJuanaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection"), c => c.CommandTimeout(180))
                //options.UseSqlite($"Data Source={DbPath}")
            );
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));            

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<LuceneSettings>(c => configuration.GetSection("LuceneSettings"));
            services.AddTransient<ILuceneService, LaJuanaLuceneContext>();
            services.Configure<DirectoryIconSettings>(configuration.GetSection("DirectoryIconSettings"));
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IAposeService, AposeService>();
            services.AddTransient<IMineTypeService, MineTypeService>();
            return services;
        }

    }
}
