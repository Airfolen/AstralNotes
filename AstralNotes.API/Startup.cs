using System;
using System.IO;
using Astral.Extensions.AspNetCore.Mvc.Swagger;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Domain;
using AstralNotes.Utils.DiceBearAvatars.Extensions;
using AstralNotes.Utils.Documents.Extensions;
using AstralNotes.Utils.FileStore;
using AstralNotes.Utils.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace AstralNotes.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
           
        public void ConfigureServices(IServiceCollection services)
        {
            //Database
            services.AddDbContext<NotesContext>(builder =>
            {
                builder.UseNpgsql(Configuration["ConnectionStrings:Database"],
                    options => options.MigrationsAssembly("AstralNotes.API"));
            });  
            
            services.AddScoped<IDataInitializer, DataInitializer>();
            
            //Services
            services.Initialization();
            services.AddDiceBearAvatars();
            services.AddITextSharpDocument();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Authentication
            services.AddIdentity<User, IdentityRole>(opts => {
                    opts.Password.RequiredLength = 5; 
                    opts.Password.RequireNonAlphanumeric = false; 
                    opts.Password.RequireLowercase = false; 
                    opts.Password.RequireUppercase = false; 
                    opts.Password.RequireDigit = false; 
                })
                .AddEntityFrameworkStores<NotesContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
            });
            
            //MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);; 
            
            services.AddLocalFileStore(characteristics =>
            {
                characteristics.RootPath = Path.Combine(Environment.ContentRootPath, "LocalFileStore");
            });
            
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            
            app.UseSwagger("PizzaDelivery");
            
            app.UseCors("AllowAll");

            app.UseStaticFiles();
            
            app.UseCookiePolicy();
            
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();  
        }
    }
}