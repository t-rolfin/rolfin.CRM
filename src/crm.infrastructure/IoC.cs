﻿using crm.common;
using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using crm.domain.Services;
using crm.infrastructure.Identity;
using crm.infrastructure.Logging;
using crm.infrastructure.QueryRepositories;
using crm.infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure
{
    public static class IoC
    {
        public static IServiceCollection Infrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(x =>
            {
                return new LeadConnString(configuration.GetConnectionString("LeadConnectionString"));
            });

            services.AddDbContext<LeadDbContext>();
            services.AddTransient<IUnitOfWork, LeadDbContext>();
            services.AddTransient<ILeadRepository, LeadRepository>();
            services.AddTransient<IRepository<Lead>, LeadRepository>();
            services.AddTransient<ILeadService, LeadService>();
            services.AddTransient<ILeadQueryRepository, LeadQueryRepository>();

            services.AddTransient<ILoggerManager, LoggerManager>();

            return services;
        }

        public static IServiceCollection Authentification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtTokenFactory, JwtTokenFactory>();

            return services;
        }
    }
}
