using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.MvcWeb.Hubs;
using ASample.NetCore.MvcWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.MvcWeb
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
            services.AddCors();
            services.AddSingleton<UserServices>();
            services.AddSingleton<DrawServices>();
            services.AddSingleton<ChartJsServices>();
            services.AddSingleton<FileServices>();
            services.AddSignalR(options =>
            {
                options.HandshakeTimeout = TimeSpan.FromMinutes(3);
                options.KeepAliveInterval = TimeSpan.FromHours(1);
                options.EnableDetailedErrors = true;
            }).AddMessagePackProtocol();

            //services.AddSignalR().AddHubOptions<ChatHub>(options =>
            //{
            //    options.HandshakeTimeout = TimeSpan.FromMinutes(3);
            //    options.KeepAliveInterval = TimeSpan.FromHours(1);
            //    options.EnableDetailedErrors = true;
            //});

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });
           
            app.UseSignalR(routes =>
            {
                //routes.MapHub<ChatHub>("/chatHub");
                routes.MapHub<ChatHub>("/chatHub", options =>
                {
                    options.WebSockets.CloseTimeout = TimeSpan.FromMilliseconds(500);
                    options.ApplicationMaxBufferSize = 0;
                    options.TransportMaxBufferSize = 0;
                });

                routes.MapHub<DrawHub>("/drawHub", options =>
                {
                    options.WebSockets.CloseTimeout = TimeSpan.FromMilliseconds(500);
                    options.ApplicationMaxBufferSize = 0;
                    options.TransportMaxBufferSize = 0;
                });

                routes.MapHub<ChartJsHub>("/chartJsHub", options =>
                {
                    options.WebSockets.CloseTimeout = TimeSpan.FromMilliseconds(500);
                    options.ApplicationMaxBufferSize = 0;
                    options.TransportMaxBufferSize = 0;
                });

                routes.MapHub<FileHub>("/fileHub", options =>
                {
                    options.WebSockets.CloseTimeout = TimeSpan.FromMilliseconds(500);
                    options.ApplicationMaxBufferSize = 0;
                    options.TransportMaxBufferSize = 0;
                });
            });
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
