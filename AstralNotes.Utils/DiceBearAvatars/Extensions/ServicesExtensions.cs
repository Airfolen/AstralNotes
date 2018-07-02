using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Utils.DiceBearAvatars.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Добавление Dice Bear Avatars в DI
        /// </summary>
        public static IServiceCollection AddDiceBearAvatars(this IServiceCollection services)
        {
            services.AddScoped<IAvatarProvider, AvatarProvider>();
         
            return services;
        }
    }
}