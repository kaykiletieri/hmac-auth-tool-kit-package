using System.Security.Cryptography;
using System.Text;

namespace HmacAuthToolkit;
public class HmacAuthenticationService
{
    private readonly string _secretKey;

    public HmacAuthenticationService(string secretKey)
    {
        _secretKey = secretKey;
    }

    public string GenerateToken(string message, IDictionary<string, string>? customClaims = null)
    {
        UTF8Encoding encoding = new();
        byte[] keyBytes = encoding.GetBytes(_secretKey);
        byte[] messageBytes = encoding.GetBytes(message);
        byte[] hashBytes;

        using (HMACSHA256 hmacsha256 = new(keyBytes))
        {
            hashBytes = hmacsha256.ComputeHash(messageBytes);
        }

        string token = Convert.ToBase64String(hashBytes);

        if (customClaims != null)
        {
            foreach (KeyValuePair<string, string> claim in customClaims)
            {
                token += $"|{claim.Key}:{claim.Value}";
            }
        }

        return token;
    }

    public bool ValidateToken(string token, string message, out IDictionary<string, string> claims)
    {
        claims = new Dictionary<string, string>();

        string[] parts = token.Split('|');
        string providedHash = parts[0];

        string generatedToken = GenerateToken(message);
        if (providedHash != generatedToken)
        {
            return false;
        }

        for (int i = 1; i < parts.Length; i++)
        {
            string[] claimParts = parts[i].Split(':');
            if (claimParts.Length == 2)
            {
                claims[claimParts[0]] = claimParts[1];
            }
        }

        return true;
    }
}