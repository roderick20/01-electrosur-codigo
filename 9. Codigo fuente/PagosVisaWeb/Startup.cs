using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using PagosVisaWeb.Models;
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

namespace PagosVisaWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
            services.AddSession();

            var connection = Configuration.GetConnectionString("ElectrosurDB");
            services.AddDbContext<ElectrosurContext>(options => options.UseSqlServer(connection));

#if (DEBUG)
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(AuthenticationFilter));
            }).AddRazorRuntimeCompilation();
#else
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(AuthenticationFilter));
            });
#endif

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>()?.HttpContext?.User);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler("/Autenticacion/PaginaNoEncontrada");
            app.UseStatusCodePagesWithReExecute("/Autenticacion/PaginaNoEncontrada", "?statusCode={0}");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}"
                );
            });
        }
    }
}


