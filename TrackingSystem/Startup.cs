using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackingSystem.Data;
using TrackingSystemBLayer.Authentication;
using TrackingSystem.Services;
using TrackingSystemBLayer.Extensions;
using TrackingSystemBLayer.Services;
using TrackingSystemBLayer.ModelHelper;
using TrackingSystemBLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrackingSystem
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
            services.AddSingleton(Configuration);

            services.AddEntityFramework(Configuration.GetConnectionString("Entities"));
            services.AddAuthentication();

            services.AddAuthentication(option =>
            {
                //option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
            })
            .AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;

                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["JwtIssuerOptions:Issuer"],
                    ValidAudience = Configuration["JwtIssuerOptions:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtIssuerOptions:Key"])),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    
                };
            });

            services.AddIdentity<IUser<int>, IRole<int>>()
                .AddUserStore<IUser<int>>()
                .AddRoleStore<IRole<int>>()               
                .AddDefaultTokenProviders();

            services.AddTransient<AccountRepository<int>>();
            services.AddTransient<RoleRepository<int>>();
            services.AddTransient<DeviceRepository<int>>();
            services.AddTransient<LocationRepository<int>>();
            services.AddTransient<UserHelper<int>>();
            services.AddTransient<RoleHelper<int>>();
            services.AddTransient<DeviceHelper<int>>();
            services.AddTransient<LocationHelper<int>>();

            services.AddTransient<IUserStore<IUser<int>>, IdentityUserStore<int>>();
            services.AddTransient<IRoleStore<IRole<int>>, IdentityRoleStore<int>>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
