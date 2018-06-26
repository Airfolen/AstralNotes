using AstralNotes.Domain.Authentication;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Users;
using AstralNotes.Utils.Password;
using AstralNotes.Utils.Tokens;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Domain
{
    public static class ServiceInitializationExtension
    {
        public static void Initialization(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHashProvider, MD5HashProvider>(); 
            services.AddScoped<IPasswordHasher, PasswordHasher>();                
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<INoteService, NoteService>();
            
            services.AddAutoMapper(a =>
            {
                a.AddProfiles(typeof(IdentityMappingProfile));
            });
        }
    }
}
