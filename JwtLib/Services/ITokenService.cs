
using System.Security.Claims;

namespace JwtLib.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
}
