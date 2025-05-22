namespace JwtLib.Models;

public record ClaimInfo
(
    string Username,
    IEnumerable<string> Roles,
    Dictionary<string, object> AdditionalClaims
);
