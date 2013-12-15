FIFA Ultimate Team 2014 Toolkit
===============================

## Supported platforms
- .NET 4 and higher
- Silverlight 4 and higher
- Windows Phone 7.5 and higher
- Windows Store Apps

## NuGet packages

If you're targeting .NET 4.5 or .NET for Windows Store apps, you'll need:  
[HttpClient](http://www.nuget.org/packages/Microsoft.Net.Http/)  
[Async targeting pack](http://www.nuget.org/packages/Microsoft.Bcl.Async/)  

## Sample usage

[Initialization](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#initialization)  
[Login](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#login)  
[Player search](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#player-search)  
[Place bid](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#place-bid)  
[Trade status](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#trade-status)  
[Item data](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#item-data)  
[Player image](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#player-image)  
[Credits](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#credits)  
[List auction](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#list-auction)  
[Get trade pile](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#get-trade-pile)  
[Watch list](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#watch-list)  
[Purchased items](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#purchased-items)  
[Development search](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#development-search)  
[Training search](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#training-search)  
[Send to trade pile](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#send-to-trade-pile)  
[Quick sell](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#quick-sell)  
[Remove from watch list](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#remove-from-watch-list)  
[Remove from trade pile](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#remove-from-trade-pile)  
[Get pile sizes](https://github.com/trydis/FIFA-Ultimate-Team-2014-Toolkit#get-pile-sizes)  

### Initialization

```csharp
var client = new FutClient();
```

### Login

```csharp
var loginDetails = new LoginDetails("e-mail", "password", "secret answer", Platform.Ps3 /* or Platform.Xbox360 / Platform.Pc */);
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

### Trade status

Retrieves the trade status of the auctions of interest.

```csharp
var auctionResponse = await client.GetTradeStatusAsync(
    Auctions // Contains the auctions we're currently watching
    .Where(x => x.AuctionInfo.Expires != -1) // Not expired
    .Select(x => x.AuctionInfo.TradeId));

foreach (var auctionInfo in auctionResponse.AuctionInfo)
{
	// Handle the updated auction data
}
```

### Item data

Contains info such as name, ratings etc.

```csharp
var item = await client.GetItemAsync(auctionInfo);
```

### Player image

- Format: PNG
- Dimensions: 100 x 100 pixels

```csharp
var imageBytes = await client.GetPlayerImageAsync(auctionInfo);
```

### Credits

Amount of coins and unopened packs.

```csharp
var creditsResponse = await client.GetCreditsAsync();
```

### List auction

Lists an auction from a trade pile item.

```csharp
// Duration = one hour, starting bid = 150 and no buy now price
var auctionDetails = new AuctionDetails(auctionInfo.ItemData.Id);
```
```csharp
// Duration = three hours, starting bid = 200 and buy now price = 1000
var auctionDetails = new AuctionDetails(auctionInfo.ItemData.Id, AuctionDuration.ThreeHours, 200, 1000);
```
```csharp
var listAuctionResponse = await client.ListAuctionAsync(auctionDetails);
```

### Get trade pile

Gets the items in the trade pile.

```csharp
var tradePileResponse = await client.GetTradePileAsync();
```

### Watch list

Retrieves the the watch list.

```csharp
var watchlistResponse = await client.GetWatchlistAsync();
```

### Purchased items

Items that have been bought or received in gift packs.

```csharp
var purchasedItemsResponse = await client.GetPurchasedItemsAsync();
```

### Development search

All the search parameters are optional. If none are specified, you will get the 1st page of results with no filters applied.

```csharp
var searchParameters = new DevelopmentSearchParameters
{
    Page = 1,
    Level = Level.Gold,
    DevelopmentType = DevelopmentType.Healing,
};

var searchResponse = await client.SearchAsync(searchParameters);
foreach (var auctionInfo in searchResponse.AuctionInfo)
{
    // Handle auction data
}
```

### Training search

All the search parameters are optional. If none are specified, you will get the 1st page of results with no filters applied.

```csharp
 var searchParameters = new TrainingSearchParameters
{
    Page = 1,
    Level = Level.Gold,
    TrainingType = TrainingType.ChemistryStyles,
};

var searchResponse = await client.SearchAsync(searchParameters);
foreach (var auctionInfo in searchResponse.AuctionInfo)
{
    // Handle auction data
}
```

### Send to trade pile

Sends an item to the trade pile (transfer market) 

```csharp
var sendToTradePileResponse = await client.SendItemToTradePileAsync(itemData);
```

### Quick sell

Quick sell an item at discard value.

```csharp
var quickSellResponse = await client.QuickSellItemAsync(ItemData.Id);
```

### Remove from watch list

Removes an auction from the watch list.

```csharp
await client.RemoveFromWatchlistAsync(auctionInfo);
```

### Remove from trade pile

Removes an auction from the trade pile.

```csharp
await client.RemoveFromTradePileAsync(auctionInfo);
```

### Get pile sizes

Gets the trade pile and watch list sizes.

```csharp
var pileSizeResponse = await client.GetPileSizeAsync();
```