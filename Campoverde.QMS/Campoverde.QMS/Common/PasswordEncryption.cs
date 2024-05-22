using System.Security.Cryptography;
using System.Text;

namespace Campoverde.QMS.Common;

public class PasswordEncryption
{
    public static string HashPassword(string password)
    {
        // Generate a salt
        byte[] salt = GenerateSalt();

        // Hash the password with the salt
        byte[] hash = HashPasswordWithSalt(password, salt);

        // Combine salt and hash
        byte[] saltAndHash = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, saltAndHash, 0, salt.Length);
        Array.Copy(hash, 0, saltAndHash, salt.Length, hash.Length);

        // Convert to base64 for storage
        return Convert.ToBase64String(saltAndHash);
    }

    private static byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    private static byte[] HashPasswordWithSalt(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];

        Array.Copy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
        Array.Copy(salt, 0, passwordWithSaltBytes, passwordBytes.Length, salt.Length);

        return SHA256.HashData(passwordWithSaltBytes);
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        byte[] saltAndHashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[16];
        Array.Copy(saltAndHashBytes, 0, salt, 0, 16);

        byte[] hashToVerify = HashPasswordWithSalt(password, salt);

        for (int i = 0; i < hashToVerify.Length; i++)
        {
            if (saltAndHashBytes[i + 16] != hashToVerify[i])
            {
                return false;
            }
        }

        return true;
    }
}
