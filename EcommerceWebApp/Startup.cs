using Application;
using Application.Models;
using AspNetCoreRateLimit;
using Domain.Entities;
using EcommerceWebApp.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(assembly);
            services.AddAutoMapper(typeof(Startup));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddInfrastructureServices(Configuration);
            services.AddApplicationServices();
            services.AddMemoryCache();
            services.AddTransient<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddDbContext<EcomDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("EcomConnectionString")));
            services.AddControllersWithViews(option => option.Filters.Add(new ApiExceptionFilter()));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<EcomDbContext>()
            .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                     opt.TokenLifespan = TimeSpan.FromHours(1));
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(appSettingsSection);
            services.AddSwaggerGen(c =>
            {
                var response = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put * *_ONLY_ * *your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(response.Reference.Id, response);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { response, Array.Empty<string>() }
                    });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceWebAPI", Version = "v1" });
            });
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecretKey)),
                    ValidateLifetime = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options => {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-Ip";
                options.ClientIdHeader = "X-Client-Ip";
                options.GeneralRules = new List<RateLimitRule>{
                    new RateLimitRule{

                        Endpoint = "*",
                        Period = "10s",
                        Limit = 5,
                    }
                };
                              
            });
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddFluentValidation(s => { s.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
            services.AddMvc(option => option.EnableEndpointRouting = false);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceWebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
