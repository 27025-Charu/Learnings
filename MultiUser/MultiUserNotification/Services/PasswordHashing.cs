using System.Security.Cryptography;
using System.Text;

namespace MultiUserNotification.Services
{
    internal class PasswordHashing
    {
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 600000; // High iteration count to resist brute-force
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
        public static string HashPassword(string password)
        {
            // 1. Generate a cryptographically secure random salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // 2. Derive the hash using PBKDF2
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                Algorithm,
                HashSize
            );

            // 3. Combine salt and hash into a single string for easy database storage
            // Format: [Iterations].[Salt].[Hash]
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        internal static bool PassVerification(string storedHash, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            try
            {
                // 1. Extract the iterations, salt, and original hash string components
                string[] parts = storedHash.Split('.');
                if (parts.Length != 3)
                {
                    return false;
                }

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] originalHash = Convert.FromBase64String(parts[2]);

                // 2. Process the newly entered password with the exact same parameters
                byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    iterations,
                    Algorithm,
                    originalHash.Length
                );

                // 3. Fixed-time validation prevents side-channel timing attacks
                return CryptographicOperations.FixedTimeEquals(originalHash, testHash);
            }
            catch
            {
                // Protect against malformed input or string manipulation errors
                return false;
            }
        }
    }
}
