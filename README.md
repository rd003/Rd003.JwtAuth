# Rd003.JwtAuth

A lightweight, easy-to-use JWT authentication library for .NET applications. Simplify JWT token generation and validation in your ASP.NET Core projects with minimal configuration.

## Features

- **🚀 Easy Integration** - Add JWT authentication with just a few lines of code
- **🔐 Secure by Default** - Uses HMAC SHA256 algorithm for token signing
- **🎯 Flexible Claims** - Support for custom claims, roles, and user data
- **⚡ High Performance** - Minimal overhead with efficient token generation
- **🛡️ Production Ready** - Built with security best practices
- **📦 Zero Dependencies** - Uses only Microsoft's official JWT libraries 

## Installation

### Package Manager Console

```bash
Install-Package Rd003.JwtAuth
```

### .NET CLI

```bash
dotnet add package Rd003.JwtAuth
```

## Quick Start

### 1. Make these changes in `Program.cs`

```cs
using JwtLib.Extensions;

// service section
int timeOutInMinutes=15;
builder.Services.AddJwtAuthentication("validIssuer", "validAudience", "secret", timeOutInMinutes);
```

- **validIssuer**: URL of the issuer of the JWT (eg: http://localhost:5000)

- **validReciever**: URL of the reciever of the JWT (eg: http://localhost:4200)

- **secret**: 32 character long secure string. Make sure this key is strong enough and store it in things like `azure key vault` so that you can easily rotate it.

- **timeoutInMinutes**: ExpiryTime of the token in minutes. It must be short-lived (approximately 15 to 20 minutes). You need to use Refresh-Token for refreshing the token, which is not a part of this library yet.

**Note:** Make sure not to hard code these values. Better to store them in the `appsettings.json` except `secret`, secret must be stored in the things like `azure key vault`.

### 3. How to generate JWT?

```cs
using JwtLib.Services;

public class AccountsController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AccountsController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<IActionResult> Login()
    {
        // generating the JWT
        string username = "rd003";
        List<string> roles = ["admin"];

        Dictionary<string, object> additionalClaims = [];
        additionalClaims.Add("email", "rd003@example.com");
        additionalClaims.Add("foo", "bar");

        var jwt = _tokenService.GenerateAccessToken(username, roles, additionalClaims);
    
        // remaining code
    }

}
```

Note: `roles` and `additionalClaims` are optional parameters.

### Paramters of GenerateAccessToken

|ParameterName|Type| Required|
|--------------|---|---|
|username|string|Yes|
|roles|IEnumerable<string>|No|
|additionalClaims|Dictionary<string, object>|No|

---

⭐ If you find this library helpful, please consider giving it a star on GitHub!