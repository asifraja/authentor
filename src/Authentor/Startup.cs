using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentor.AuthorizationRequirements;
using Authentor.IdentityData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authentor
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthentorDbContext>(confir =>
            {
                confir.UseInMemoryDatabase("MemoryDatabase");
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AuthentorDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Authentor.Cookie";
                config.LoginPath = "/Home/Login";
            });

            // Following is an example of using the AuthorizationPolicyBuilder
            services.AddAuthorization(config =>
            {
                //    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //    var defaultAithPolicy = defaultAuthBuilder
                //    .RequireAuthenticatedUser()
                //    //.RequireClaim(ClaimTypes.DateOfBirth)
                //    .Build();
                
                config.AddPolicy("Claim.DoB", policyBuilder =>
                        {
                            policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                        });
            });

            // Add Singlton if passing DB Context or other services
            services.AddScoped<IAuthorizationHandler, CustomRequirementClaimHandler>();

            services.AddControllersWithViews(config =>
            {
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAithPolicy = defaultAuthBuilder
                //.RequireAuthenticatedUser()
                //.Build();

                ////// example of Authorization filter
                //config.Filters.Add(new AuthorizeFilter(defaultAithPolicy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
