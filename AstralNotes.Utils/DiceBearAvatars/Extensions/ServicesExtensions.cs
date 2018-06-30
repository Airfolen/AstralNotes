using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Utils.DiceBearAvatars.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Добавляет Dice Bear Avatars
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAvatars(this IServiceCollection services)
        {
            services.AddScoped<IAvatarProvider, AvatarProvider>();
         
            return services;
        }
    }
}