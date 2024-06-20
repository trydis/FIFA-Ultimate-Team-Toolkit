using Microsoft.Extensions.Configuration;
using System.Net;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Models.Auth;
using UltimateTeam.Toolkit.Models.Generic;
using UltimateTeam.Toolkit.Services;
using UltimateTeam.Toolkit.Tests.Helpers;

ITwoFactorCodeProvider provider = new Auth();
CookieContainer cookieContainer = CookieHandler.LoadCookiesFromJson("cookie.json");

FutClient client = new FutClient(cookieContainer);
LoginDetails loginDetails = new LoginDetails("<user>", "<password>", Platform.Ps5, AppVersion.WebApp);
LoginResponse loginResponse = await client.LoginAsync(loginDetails, provider);

CookieHandler.SaveCookiesToJson(cookieContainer, "cookie.json");
