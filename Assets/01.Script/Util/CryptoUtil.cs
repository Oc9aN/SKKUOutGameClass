using System.Security.Cryptography;
using System.Text;

public class CryptoUtil
{
    public static string Encryption(string plainText, string salt)
    {
        SHA256 sha256 = SHA256.Create();
        
        // 운영체제 혹은 언어별로 string을 표현하는 방식이 다 다르므로 UTF-8버전의 바이트 배열로 바꿔야한다.
        byte[] bytes = Encoding.UTF8.GetBytes(plainText + salt);
        byte[] hash = sha256.ComputeHash(bytes);
        
        string resultText = string.Empty;
        foreach (var b in hash)
        {
            resultText += b.ToString("X2");
        }
        
        return resultText;
    }

    public static bool Verify(string plainText, string hash, string salt = "")
    {
        return Encryption(plainText, salt) == hash;
    }
}