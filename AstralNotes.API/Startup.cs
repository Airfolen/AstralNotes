using System;
using System.Collections.Generic;
using System.IO;
using AstralNotes.Database;
using AstralNotes.Utils.Filters;
using AstralNotes.Utils.JwtOptions;
using AstralNotes.Utils.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace AstralNotes.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public IHostingEnvironment Environment { get; }
        
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
           
            //Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthenticationOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthenticationOptions.Audience,
                        
                        ValidateLifetime = true,

                        IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            //MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });
            
            //CORS
            services.AddCors(options => options.AddPolicy("AllowAll", 
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            
            services.AddSwagger(
                xmlComments: new List<string>{"AstralNotes.API.xml"}, 
                info: new Info {
                    Version = "v1",
                    Title = "Core API"
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (Environment.IsDevelopment() || Environment.IsStaging())
            {
                app.UseSwagger("Core API v1");
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