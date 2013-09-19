FIFA Ultimate Team 2014 Toolkit
===============================

## Supported platforms
- .NET 4 and higher
- Silverlight 4 and higher
- Windows Phone 7.5 and higher
- Windows Store Apps

## Sample usage

[Initialization](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#initialization)  
[Login](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#login)  
[Player search](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#player-search)  
[Place bid](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#place-bid)  
[Item data](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#item-data)  
[NuGet packages](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#nuget-packages)  

### Initialization

```csharp
var client = new FutClient();
```

### Login

```csharp
var loginDetails = new LoginDetails("e-mail", "password", "secret answer", Platform.Ps3 /* or Platform.Xbox360 */);
var loginResponse = await client.LoginAsync(loginDetails);
```

### Player search

All the search parameters are optional. If none are specified, you will get the 1st page of results with no filters applied.

```csharp
var searchParameters = new PlayerSearchParameters
{
    Page = 1,
    Level = Level.Gold,
    ChemistryStyle = ChemistryStyle.Sniper,
    League = League.BarclaysPremierLeague,
    Nation = Nation.Norway,
    Position = Position.Striker,
    Team = Team.ManchesterUnited
};

var searchResponse = await client.SearchAsync(searchParameters);
foreach (var auctionInfo in searchResponse.AuctionInfo)
{
	// Handle auction data
}
```

### Place bid

Passing the amount explicitly:

```csharp
var auctionResponse = await client.PlaceBidAsync(auctionInfo, 150);
```

Place the next valid bid amount:

```csharp
var auctionResponse = await client.PlaceBidAsync(auctionInfo);
```

### Item data

Contains info such as name, ratings etc.

```csharp
var item = await client.GetItemAsync(auctionInfo);
```

### NuGet packages

If you're targeting .NET 4.5 or .NET for Windows Store apps, you'll need:  
[HttpClient](http://www.nuget.org/packages/Microsoft.Net.Http/)  
[Async targeting pack](http://www.nuget.org/packages/Microsoft.Bcl.Async/)
