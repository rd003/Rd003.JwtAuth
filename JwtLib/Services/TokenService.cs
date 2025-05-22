using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtLib.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtLib.Services;

public class TokenService : ITokenService
{
    private readonly int _timoutInMinute;
    private readonly string _secret;
    private readonly string _validAudience;
    private readonly string _validIssuer;

    public TokenService(string validIssuer, string validAudience, string secret, int timoutInMinute)
    {
        _validAudience = validAudience;
        _validIssuer = validIssuer;
        _secret = secret;
        _timoutInMinute = timoutInMinute;
    }

    public string GenerateAccessToken(ClaimInfo claimInfo)
    {
        if (claimInfo == null)
        {
            throw new ArgumentNullException("ClaimInfo");
        }

        if (string.IsNullOrWhiteSpace(claimInfo.Username))
        {
            throw new ArgumentNullException("Username");
        }

        List<Claim> claims = [
                 new (ClaimTypes.Name, claimInfo.Username),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             // unique identifier for jwt
             ];

        // adding roles to claims

        foreach (var role in claimInfo.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // additional claims

        foreach (var claim in claimInfo.AdditionalClaims)
        {
            claims.Add(new Claim(claim.Key, claim.Value.ToString() ?? ""));
        }

        return GenerateAccessTokenFromClaims(claims);
    }

    private string GenerateAccessTokenFromClaims(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create a symmetric security key using the secret key from the configuration.
        var authSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(_secret));


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _validIssuer,
            Audience = _validAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(_timoutInMinute),
            SigningCredentials = new SigningCredentials
                          (authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create a symmetric security key using the secret key from the configuration.
        var authSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(_secret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _validIssuer,
            Audience = _validAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(_timoutInMinute),
            SigningCredentials = new SigningCredentials
                          (authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
