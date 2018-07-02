using System;
using System.IO;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Domain;
using AstralNotes.Utils.DiceBearAvatars.Extensions;
using AstralNotes.Utils.FileStore;
using AstralNotes.Utils.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

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
            });
            
            //CORS
            services.AddCors(options => options.AddPolicy("AllowAll", 
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            
            //Swagger
            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Astral Notes",
                    Description = "ASP.NET Core Web API"
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "AstralNotes.API.xml");
                a.IncludeXmlComments(xmlPath);
            });
            
            services.AddLocalFileStore(characteristics =>
            {
                characteristics.RootPath = Path.Combine(Environment.ContentRootPath, "LocalFileStore");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (Environment.IsDevelopment() || Environment.IsStaging())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.DocExpansion("full");
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API v1");
                    options.RoutePrefix = "swagger";
                });
                loggerFactory.AddDebug();
            }
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            
            app.UseCors("AllowAll");

            app.UseStaticFiles();
            
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();  
        }
    }
}