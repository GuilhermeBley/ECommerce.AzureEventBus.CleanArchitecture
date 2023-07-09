using System.Text;

namespace Ecommerce.Catalog.Core.Security;

public class HashConvert
{
    public static HashedPassword CreateHashedPassword(string password)
    {
        string salt = CreateSalt();
        string hash = GetBase64Password(password, salt);

        return new HashedPassword(hash, salt);
    }

    public static bool IsValidPassword(string passwordToCheck, string hashCompare, string saltCompare)
    {
        var hashPasswordToCheck = GetBase64Password(passwordToCheck, saltCompare);

        return hashPasswordToCheck == hashCompare;
    }

    private static string GetBase64Password(string password, string salt)
    {
        byte[] passwordbytes = Encoding.Unicode.GetBytes(password + salt);

        var hasher = System.Security.Cryptography.SHA256.Create();
        byte[] hashedBytes = hasher.ComputeHash(passwordbytes);

        return Convert.ToBase64String(hashedBytes);
    }

    private static string CreateSalt()
        => Guid.NewGuid().ToString();

    public class HashedPassword
    {
        public string HashBase64 { get; }
        public string Salt { get; }

        public HashedPassword(string hashBase64, string salt)
        {
            HashBase64 = hashBase64;
            Salt = salt;
        }
    }
}
