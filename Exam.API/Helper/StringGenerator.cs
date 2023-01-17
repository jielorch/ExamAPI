using System.Security.Cryptography;
using System.Text;

namespace Exam.API.Helper
{
    public static class StringGenerator
    {
        private static readonly Random random = new();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var value = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return GetSha1(value);
        }

        private static string GetSha1(string value)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
