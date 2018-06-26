using System.Security.Cryptography;
using System.Text;

namespace AstralNotes.Utils.Password
{
    public class MD5HashProvider : IPasswordHashProvider
    {
        public string Hash(string input)
        {
            var md5Hasher = new MD5CryptoServiceProvider();

            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var stringBuilder = new StringBuilder();

            foreach (var dataByte in data)
                stringBuilder.Append(dataByte.ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}