using AstralNotes.Domain.Avatars;
using AstralNotes.Domain.Notes;
using AstralNotes.Domain.Users;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Domain
{
    public static class ServiceInitializationExtension
    {
        /// <summary>
        /// Добавление сервисов в DI
        /// </summary>
        public static void Initialization(this IServiceCollection services)
        {
            services.AddScoped<IAvatarService, AvatarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            
            services.AddAutoMapper(a =>
            {
                a.AddProfiles(typeof(MappingProfile));
            });
        }
    }
}
