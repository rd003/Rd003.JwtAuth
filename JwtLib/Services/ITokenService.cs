
using System.Security.Claims;

namespace JwtLib.Services;

public interface ITokenService
{
    string GenerateAccessToken(string username, IEnumerable<string>? roles = null, Dictionary<string, object>? additionalClaims = null);
}
