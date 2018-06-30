using System;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Utils.FileStore
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddLocalFileStore(this IServiceCollection services, Action<LocalFileStorageOptions> setup)
        {
            var options = new LocalFileStorageOptions();
            setup?.Invoke(options);
            services.AddSingleton(options);

            services.AddScoped<IFileStorage, LocalFileStorage>();

            return services;
        }
    }
}