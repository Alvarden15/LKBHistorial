using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.MvcContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


namespace LKBHistorial
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
           

            services.AddMvc();
            
            //services.AddDbContextPool<MvcContext>(options=>options.UseSqlServer("Server=tcp:lkb.database.windows.net,1433;Initial Catalog=LKBData;Persist Security Info=False;User ID=lkbadmin;Password=LKBHistorial!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",providerOptions=>providerOptions.EnableRetryOnFailure()));
            services.AddDbContextPool<MvcContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),providerOptions=>providerOptions.EnableRetryOnFailure()));           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddDefaultIdentity<MvcContext>();

            services.Configure<IdentityOptions>(o=>
            {
                o.Password.RequireDigit=true;
                o.Password.RequiredLength=6;
                o.Password.RequiredUniqueChars=1;


                o.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(10);
                o.Lockout.MaxFailedAccessAttempts=5;
                o.Lockout.AllowedForNewUsers=true;


                o.User.AllowedUserNameCharacters=
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                o.User.RequireUniqueEmail=false;


            });

            services.ConfigureApplicationCookie(o=>{
                o.Cookie.HttpOnly=true;
                o.ExpireTimeSpan=TimeSpan.FromMinutes(10);


            });

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
