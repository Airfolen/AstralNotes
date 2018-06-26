using System;
using System.Linq;

namespace AstralNotes.Utils.Password
{
    public static class Randomizer
    {
        public static string GetString(int length)
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(elements => elements[random.Next(elements.Length)]).ToArray());
        }
    }
}