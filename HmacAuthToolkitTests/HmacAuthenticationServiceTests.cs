using HmacAuthToolkit;

namespace HmacAuthToolkitTests;
public class HmacAuthenticationServiceTests
{
    private const string SecretKey = "supersecretkey";
    private readonly HmacAuthenticationService _hmacService;

    public HmacAuthenticationServiceTests()
    {
        _hmacService = new HmacAuthenticationService(SecretKey);
    }

    [Fact]
    public void GenerateToken_WithNoClaims_ReturnsValidToken()
    {
        string message = "testMessage";
        string token = _hmacService.GenerateToken(message);

        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void GenerateToken_WithClaims_ReturnsValidTokenWithClaims()
    {
        string message = "testMessage";
        Dictionary<string, string> claims = new()
            {
                { "claim1", "value1" },
                { "claim2", "value2" }
            };
        string token = _hmacService.GenerateToken(message, claims);

        Assert.Contains("claim1:value1", token);
        Assert.Contains("claim2:value2", token);
    }

    [Fact]
    public void ValidateToken_WithValidToken_ReturnsTrueAndClaims()
    {
        string message = "testMessage";
        Dictionary<string, string> claims = new()
            {
                { "claim1", "value1" },
                { "claim2", "value2" }
            };
        string token = _hmacService.GenerateToken(message, claims);

        bool isValid = _hmacService.ValidateToken(token, message, out IDictionary<string, string>? extractedClaims);

        Assert.True(isValid);
        Assert.Equal(claims, extractedClaims);
    }

    [Fact]
    public void ValidateToken_WithInvalidToken_ReturnsFalse()
    {
        string message = "testMessage";
        string invalidToken = "invalidToken";

        bool isValid = _hmacService.ValidateToken(invalidToken, message, out IDictionary<string, string> _);

        Assert.False(isValid);
    }
}
