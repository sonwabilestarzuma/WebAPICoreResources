using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using WebApiCoreResources.Data;
using WebApiCoreResources.Models;

namespace WebApiCoreResources
{
    public class Startup
    {
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<CampContext>();
            services.AddScoped<ICampRepository, CampRepository>();
            services.AddTransient<campDbInitializer>();
            services.AddTransient<CampIdentityInitializer>();
           
            services.AddSingleton(_config);
            services.AddDbContext<CampContext>(ServiceLifetime.Scoped)
              .AddIdentity<CampUser, IdentityRole>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, campDbInitializer seeder, CampIdentityInitializer seed, ILoggerFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
            }
            else
            {
                factory.AddDebug(LogLevel.Error);


                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseCookiePolicy();

                Mapper.Initialize(Config =>
                {
                    Config.CreateMap<TalkModel, Talks>().ReverseMap();
                });
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

                seeder.EnsureSeedData().Wait();
                seed.EnsureSeedData().Wait();
            }
        }
    }
}