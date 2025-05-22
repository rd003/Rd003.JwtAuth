# Rd003.JwtAuth

description

## How to use it

1. Install package

2. Make these changes in `Program.cs`

```cs
using JwtLib.Extensions;

// service section
int timeOutInMinutes=15;
builder.Services.AddJwtAuthentication("validAudience", "validIssuer", "secret", timeOutInMinutes);
```

- **validIssuer**: URL of the issuer of the JWT (eg: http://localhost:5000)
- **validReciever**: URL of the reciever of the JWT (eg: http://localhost:4200)
- **secret**: 32 character long secure string
- **timeoutInMinutes**: ExpiryTime of the token in minutes

3. How to generate JWT

```cs
using JwtLib.Services;

// generating the JWT
string username = "rd003";
List<string> roles = ["admin"];

Dictionary<string, object> additionalClaims = [];
additionalClaims.Add("email", "rd003@example.com");
additionalClaims.Add("foo", "bar");

var jwt = _tokenService.GenerateAccessToken(username, roles, additionalClaims);
```

Note: `roles` and `additionalClaims` are optional parameters.