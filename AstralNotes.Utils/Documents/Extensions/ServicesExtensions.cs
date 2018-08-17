using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Utils.Documents.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Добавление iTextSharp Dcument в DI
        /// </summary>
        public static IServiceCollection AddITextSharpDocument(this IServiceCollection services)
        {
            services.AddScoped<ITextSharpDocument, TextSharpDocument>();
         
            return services;
        }
    }
}