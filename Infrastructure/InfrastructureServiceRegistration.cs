using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.Configure<IdentityOptions>(option =>
            {

                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@$+";
                option.User.RequireUniqueEmail = true;

                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                option.Lockout.AllowedForNewUsers = true;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                option.Lockout.MaxFailedAccessAttempts = 5;



            });
            return services;
        }
    }

}
