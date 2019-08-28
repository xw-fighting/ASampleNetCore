using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.WebSite.Filters;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.WebSite
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //var hangfireConnStr = Configuration["sql:connectionString"];
            services.AddSignalR();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["sql:connectionString"]));
            services.AddHangfireServer();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var options = new DashboardOptions
            {
                //AuthorizationFilters = new[]
                //{
                //    new AuthorizationFilter { Users = "admin, superuser", Roles = "advanced" },
                //    new ClaimsBasedAuthorizationFilter("name", "value")
                //}
                Authorization = new[] { new HFDashboardAuthFilter() }
            };
            app.UseHangfireDashboard("/hangfire", options);
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"),"0 3 * * *");


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
