using System.Text.RegularExpressions;

namespace AstralNotes.Utils.Password
{
    /// <summary>
    /// Сервис валидации пароля
    /// </summary>
    public class PasswordValidator : IPasswordValidator
    {
        private const int MinLength = 6;

        /// <summary>
        /// Проверка пароля на валидность
        /// </summary>
        public bool Validate(string password) //ToDo Нормальная подробная валидация
        {
            if (string.IsNullOrEmpty(password) || password.Length < MinLength)
                return false;

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$";

            return Regex.IsMatch(password, pattern);
        }
    }
}
