// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using System.Text.Json;
using ated_id.Services;
using IdentityServer4;
using Auth.Persistence;
using Auth.Persistence.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ated_id
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", options => {
                    options.RequireRole(Roles.Admin);
                });
            });
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Popugs", "AdminOnly");
            });
            services.AddCors(options =>
            {
                var allowedOrigins = Config.Clients
                    .Where(c => c.AllowedCorsOrigins != null)
                    .SelectMany(c => c.AllowedCorsOrigins);
                options.AddPolicy("AllowClientCors", policyBuilder => policyBuilder
                    .SetIsOriginAllowed(origin => 
                        allowedOrigins.Contains(origin))
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddDbContext<AuthContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Popug, Role>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequiredLength = 5;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<PopugClaimsFactory>() ;
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = Configuration.GetValue<string>("IdentityIssuerUri");

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<Popug>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
            services.AddAuthentication();
            
            //
            // services.AddScoped<IProfileService, IdentityProfileService>();
            services.AddScoped<IClaimsService, PopugClaimsService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowClientCors");
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}