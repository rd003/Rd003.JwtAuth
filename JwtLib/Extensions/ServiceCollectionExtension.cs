using System.Text;
using JwtLib.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtLib.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string validIssuer, string validAudience, string secret, int timeoutInMinutes = 15)
    {
        services.AddTransient<ITokenService>(provider => new TokenService(validIssuer, validAudience, secret, timeoutInMinutes));
        services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = validAudience,
        ValidIssuer = validIssuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});
        return services;
    }
}
