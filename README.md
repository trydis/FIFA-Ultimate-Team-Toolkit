FIFA Ultimate Team 2014 Toolkit
===============================

## Sample usage

[Login](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#login)  

### Login

```csharp
var client = new FutClient();
var loginDetails = new LoginDetails("e-mail", "password", "secret answer");
await client.LoginAsync(loginDetails);
```

### NuGet packages

If you're targeting .NET 4.5 or .NET for Windows Store apps, you'll need:
http://www.nuget.org/packages/Microsoft.Net.Http/
