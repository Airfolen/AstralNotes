using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AstralNotes.Utils.JwtOptions
{
    /// <summary>
    /// Константы для Аутентификации чрез JWT-Токены
    /// </summary>
    public class AuthenticationOptions
    {
        /// <summary>
        /// Строка секретного ключа для шифрации
        /// </summary>
        private const string Key = "333999e1-7322-48a4-b4c7-92c2a9845b56";
        
        /// <summary>
        /// Издатель токена
        /// </summary>
        public const string Issuer = "AstralNotes";

        /// <summary>
        /// Потребитель токена 
        /// </summary>
        public const string Audience = "AstralNotes";

        /// <summary>
        /// Время жизни токена в днях
        /// </summary>
        public const int LifeTime = 100;


        /// <summary>
        /// Получение секретного ключа
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
} 