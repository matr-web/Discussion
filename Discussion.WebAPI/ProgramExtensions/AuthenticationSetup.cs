using Discussion.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Discussion.WebAPI.ProgramExtensions;

/// <summary>
/// Authentication Configuration.
/// </summary>
public static class AuthenticationSetup
{
    public static IServiceCollection RegisterAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var authenticationSettings = new AuthenticationSettings();

        builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

        services.AddSingleton(authenticationSettings);

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false; // We do not force only the https protocol from the client.
            cfg.SaveToken = true; // The given token should be saved on the server side for authentication.
                                  // Validation parameters to check if the parameters sent by the client match what the server knows.
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JWTIssuer, // Token Issuer.
                ValidAudience = authenticationSettings.JWTIssuer, // Which entities can use this token (same value because we generate the token within our application).
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JWTKey)), // Private key generated from JwtKey value.
            };
        });

        return services;
    }
}