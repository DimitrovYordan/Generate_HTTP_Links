using System.Security.Cryptography;

namespace Task_Progress_Generate_HTTP_Links.Services
{
    public class CodeGenerator
    {
        private static readonly char[] _chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string GenerateShortUrl(int length = 6)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = _chars[bytes[i] % _chars.Length];
            }

            return new string(result);
        }

        public static string GenerateSecretUrl(int bytesLength = 16) 
            => Convert.ToHexString(RandomNumberGenerator.GetBytes(bytesLength));
    }
}
