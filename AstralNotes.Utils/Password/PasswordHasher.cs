using System.Text;

namespace AstralNotes.Utils.Password
{
    /// <summary>
    /// Сервис хэширования пароля
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IPasswordHashProvider _hashProvider;

        public PasswordHasher(IPasswordHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        /// <summary>
        /// Получения хэша из строки
        /// </summary>
        /// <returns>Хэш пароля</returns>
        public string Hash(string password)
        {
            return _hashProvider.Hash(password);
        }

        /// <summary>
        /// Сравнение за константное время, для избежания взлома по времени
        /// </summary>
        public bool VerifyHashedPassword(string password1, string password2)
        {
            var array1 = Encoding.Default.GetBytes(password1);
            var array2 = Encoding.Default.GetBytes(password2);
            var diff = array1.Length ^ array2.Length;
            for (var i = 0; i < array1.Length && i < array2.Length; i++)
                diff |= array1[i] ^ array2[i];
            return diff == 0;
        }
    }
}
