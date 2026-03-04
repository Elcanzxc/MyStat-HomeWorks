namespace InvoiceProject.Config;

public class JWTConfig
{
    public const string SectionName = "JwtOptions";

    public string SecretKey { get; set; } = string.Empty;
    public string RefreshTokenSecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
}
