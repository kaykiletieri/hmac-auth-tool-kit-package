
# HmacAuthToolkit

HmacAuthToolkit is a package designed to simplify the process of HMAC-based authentication, allowing for easy configuration of standard HMAC authentication as well as customization with additional claims.

## Features

-   Easy to configure HMAC authentication
-   Customizable claims for enhanced security
-   Lightweight and efficient

## Installation

You can install the HmacAuthToolkit package via NuGet:

```bash
dotnet add package HmacAuthToolkit 
```
Or you can add it directly to your project file:

```xml
<PackageReference Include="HmacAuthToolkit" Version="1.0.0" />
```

## Usage

### Basic Usage

Here's a simple example of how to use HmacAuthToolkit to generate and validate HMAC tokens.

```cs
using HmacAuthToolkit;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var secretKey = "supersecretkey";
        var hmacService = new HmacAuthenticationService(secretKey);
        var message = "testMessage";
        // Generate token
        var token = hmacService.GenerateToken(message);
        Console.WriteLine($"Generated Token: {token}");
        // Validate token
        var isValid = hmacService.ValidateToken(token, message, out var claims);
        Console.WriteLine($"Is Token Valid: {isValid}");
    }
}
````

### Custom Claims

You can also include custom claims when generating tokens:

```cs
using HmacAuthToolkit;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var secretKey = "supersecretkey";
        var hmacService = new HmacAuthenticationService(secretKey);

        var message = "testMessage";
        var customClaims = new Dictionary<string, string>
        {
            { "claim1", "value1" },
            { "claim2", "value2" }
        };

        // Generate token with custom claims
        var token = hmacService.GenerateToken(message, customClaims);
        Console.WriteLine($"Generated Token with Claims: {token}");

        // Validate token and extract claims
        var isValid = hmacService.ValidateToken(token, message, out var claims);
        Console.WriteLine($"Is Token Valid: {isValid}");
        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim: {claim.Key}, Value: {claim.Value}");
        }
    }
}
```
### Contributing
Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.
