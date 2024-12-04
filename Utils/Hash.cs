using System.Security.Cryptography;
using System.Text;

namespace temuruang_be.Utils;

public class Hash
{
    public static string HashPassword(string plain)
    {
        var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(plain);
        var hash = sha256.ComputeHash(bytes);
        var hashString = Convert.ToBase64String(hash);

        return hashString;
    }
}