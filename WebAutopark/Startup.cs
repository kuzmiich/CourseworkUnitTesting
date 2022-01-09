using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using WebAutopark.BusinessLayer.Extensions;
using WebAutopark.BusinessLayer.MappingProfiles;
using WebAutopark.MappingProfiles;

namespace WebAutopark
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Configure services
        public void ConfigureServices(IServiceCollection services)
        {
            // added my repositories, services, context, automapper and identity configuration
            services.AddCustomSolutionConfigs(_configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(MiddlewareServiceExtensions.GetCart);

            services.AddAutoMapper(cfg => cfg.AddProfiles(new Profile[]
            {
                new ConfigureBusinessLayerProfile(),
                new ConfigureViewModelProfile()
            }));

            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();
        }

        // Configure application and environment
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseRequestLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}