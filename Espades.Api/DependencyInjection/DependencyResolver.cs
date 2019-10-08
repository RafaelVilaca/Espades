﻿using Espades.Domain.Contracts.Services.Base;
using Espades.Domain.Contracts.UnitOfWork;
using Espades.Domain.Repository;
using Espades.Infrastructure;
using Espades.Infrastructure.Base.Repositories;
using Espades.Infrastructure.UnitiesOfWork;
using Espades.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Espades.Api.DependencyInjection
{
    public class DependencyResolver
    {
        public static void Resolve(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<EspadesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EspadesConnection"),
                    b => b.MigrationsAssembly("Espades.Api")
                    .UseRowNumberForPaging()));

            services.TryAddTransient(typeof(IService<>), typeof(Service<>));
            services.TryAddTransient<IRepository, Repository>();
            services.TryAddTransient<IUnitOfWork, EFUnitOfWork>();
            services.TryAddTransient<IUnitOfWorkFactory, EFUnitOfWorkFactory>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
