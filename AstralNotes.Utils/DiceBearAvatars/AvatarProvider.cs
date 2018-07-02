using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstralNotes.Utils.DiceBearAvatars
{
    /// <summary>
    /// Провайдер для работы с DiceBear Avatars
    /// </summary>
    public class AvatarProvider : IAvatarProvider, IDisposable
    {
        private readonly HttpClient _httpClient;

        public AvatarProvider()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"https://avatars.dicebear.com/v2/")
            };
        }
        
        /// <summary>
        /// Получение аватара
        /// </summary>
        public async Task<Stream> GetAsync(string seed)
        {
            var result = await _httpClient.GetAsync($"identicon/{seed}.svg");
            
            if (!result.IsSuccessStatusCode)
                throw new HttpRequestException($"Запрос на DiceBear Avatars API завершился ошибкой," +
                                               $" сообщение исключения:{result.Content}");
            
            return await result.Content.ReadAsStreamAsync();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}