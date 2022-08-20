using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using klevr.Concrete.EF;
using klevr.Concrete.Persistence;
using klevr.Core.Repository;
using klevr.Helpers;
using klevr.InMemoryCacheModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace klevr
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
            services.AddControllers();
            var allowedHostsArr = Configuration.GetSection("AllowedHosts").Get<string>();
            services.AddDbContext<DBContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddCors(options =>
            {
                options.AddPolicy("_myAllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins(allowedHostsArr).WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete, HttpMethods.Options).WithHeaders(HeaderNames.ContentType, HeaderNames.AccessControlAllowOrigin, HeaderNames.AccessControlRequestHeaders, HeaderNames.Authorization);
                });
            });

            //get data from app settings and init into class
            var appSettingsSection = Configuration.GetSection("CacheSettings");
            services.Configure<CacheSettings>(appSettingsSection);


            //inject repos
            services.AddTransient<IUserLimitRepository, UserLimitRepository>();

            //inject services
            services.AddScoped<ICacheService, CacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<DBContext>().Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
