using System;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Utils.FileStore
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Добавление локального хранилиша файлов в DI
        /// </summary>
        public static IServiceCollection AddLocalFileStore(this IServiceCollection services, Action<LocalFileStorageСharacteristics> setup)
        {
            var сharacteristics = new LocalFileStorageСharacteristics();
            setup?.Invoke(сharacteristics);
            services.AddSingleton(сharacteristics);

            services.AddScoped<IFileStorage, LocalFileStorage>();

            return services;
        }
    }
}