using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AstralNotes.Utils.Swagger
{
    /// <summary>
    /// Расширения для подключения Swagger
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Регистрация сервисов swagger
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="xmlComments">Список путей к файлам XML комментариев</param>
        /// <param name="additionalSettings">Доп. настройки конфигурации swagger</param>
        /// <param name="info">Информация для Swagger</param>
        /// <param name="useJwtAuthorization">Включение JWT авторизации</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, List<string> xmlComments = null,
            Action<SwaggerGenOptions> additionalSettings = null, Info info = null, bool useJwtAuthorization = true)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", info ?? new Info { Version = "v1", Title = "API", Description = "Service API" });
                
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                xmlComments?.ForEach(x => options.IncludeXmlComments(Path.Combine(basePath, x)));

                options.CustomSchemaIds(type =>
                {
                    var typeName = type.Name;
                    if (type.IsConstructedGenericType)
                    {
                        typeName = string.Concat(typeName, '_', type.GenericTypeArguments[0]);
                    }

                    return typeName;
                });

                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(type => type.FullName);
              
                if (useJwtAuthorization)
                {
                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "Авторизация с помощью JWT токена. Пример: Bearer -token-",
                        Name = "authorization",
                        In = "header",
                        Type = "apiKey"
                    });
                }

                additionalSettings?.Invoke(options);
            });

            return services;
        }

        /// <summary>
        /// Добавление Swagger middleware
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="description">Описание swagger endpoint</param>
        /// <param name="settings">Доп. настройки конфигурации swagger</param>
        /// <param name="UISettings">Доп. настройки конфигурации swagger UI</param>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder applicationBuilder, string description,
            Action<SwaggerOptions> settings = null, Action<SwaggerUIOptions> UISettings = null)
        {
            applicationBuilder.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                    swaggerDoc.Host = httpRequest.Host.Value);
                settings?.Invoke(options);
            });

            applicationBuilder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", description);
                options.RoutePrefix = "help";
                UISettings?.Invoke(options);
            });

            return applicationBuilder;
        }
    }
}
