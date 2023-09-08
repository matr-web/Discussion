namespace Discussion.Models.Models;

/// <summary>
/// Class that represents Authentication Values from appsettings.json
/// </summary>
public class AuthenticationSettings
{
    /// <summary>
    /// JWT Key.
    /// </summary>
    public string JWTKey { get; set; }

    /// <summary>
    /// JWT Expire Days.
    /// </summary>
    public int JWTExpireDays { get; set; }

    /// <summary>
    /// JWT Issuer.
    /// </summary>
    public string JWTIssuer { get; set; }
}