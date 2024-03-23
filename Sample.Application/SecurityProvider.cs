using System.Security.Cryptography;
using System.Text;
namespace Sample.Application
{
    /// <summary>
    /// Provides security operations.
    /// </summary>
    internal class SecurityProvider
    {
        private const int DEFAULT_ITERATIONS = 350000;
        private static HashAlgorithmName DefaultHashAlgorithm = HashAlgorithmName.SHA512;

        public SecurityProvider()
        {
            this.HashAlgorithm = DefaultHashAlgorithm;
            this.Iterations = DEFAULT_ITERATIONS;
        }

        private static byte[] HashPasword(byte[] password, byte[] salt, int hashLength, HashAlgorithmName hashAlgorithm, int iterations)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (salt is null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            return Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, hashLength);
        }

        public string CreateSalt(int length)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToHexString(saltBytes);

        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromHexString(salt);
            byte[] hashBytes = Convert.FromHexString(hash);

            var hashToCompare = HashPasword(passwordBytes, saltBytes, hashBytes.Length, this.HashAlgorithm, this.Iterations);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, hashBytes);
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromHexString(salt);
            int hashLength = saltBytes.Length;

            return HashPassword(password, hashLength, salt);
        }

        public string HashPassword(string password, int hashLength, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromHexString(salt);

            byte[] hashBytes = HashPasword(passwordBytes, saltBytes, hashLength, this.HashAlgorithm, this.Iterations);
            return Convert.ToHexString(hashBytes);
        }

        public HashAlgorithmName HashAlgorithm { get; set; } 
        public int Iterations { get; set; }
    }
}
