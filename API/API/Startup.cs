using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Helpers;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });

            #region jwt-auth
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = (context) =>
                    {
                        var name = context.Principal.Identity.Name;
                        if (string.IsNullOrEmpty(name)) context.Fail("Unauthorized. Please re-login.");
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion
            
            #region ioc
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IFacilityService, FacilityMaganer>();
            services.AddScoped<IContractService, ContractManager>();
            services.AddScoped<IClientService, ClientManager>();
            services.AddScoped<IItemService, ItemManager>();
            services.AddScoped<IClientContactService, ClientContactManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IClientDal, ClientDal>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IItemDal, ItemDal>();
            services.AddScoped<IClientContactDal, ClientContactDal>();
            services.AddScoped<IContractDal, ContractDal>();
            services.AddScoped<IFacilityDal, FacilityDal>();
            #endregion
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
