FIFA Ultimate Team Toolkit
===============================

[![Join the chat at https://gitter.im/trydis/FIFA-Ultimate-Team-Toolkit](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/trydis/FIFA-Ultimate-Team-Toolkit?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Supported platforms
- .NET 4.5
- Windows 8.x
- Windows Phone 8.1
- Xamarin.Android
- Xamarin.iOS

## Build status
[![Build status](https://ci.appveyor.com/api/projects/status/4owj0a485hhx1j7c/branch/master?svg=true)](https://ci.appveyor.com/project/trydis/fifa-ultimate-team-toolkit/branch/master)

## NuGet package

[Install-Package UltimateTeam.Toolkit](https://www.nuget.org/packages/UltimateTeam.Toolkit/)

## Sample usage

[Initialization](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#initialization)  
[Login](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#login)  
[Player search](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#player-search)  
[Place bid](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#place-bid)  
[Trade status](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#trade-status)  
[Item data](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#item-data)  
[Player image](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#player-image)  
[Club image](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#club-image)  
[Nation image](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#nation-image)  
[Credits](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#credits)  
[List auction](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#list-auction)  
[Get trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-trade-pile)  
[Watch list](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#watch-list)  
[Purchased items](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#purchased-items)  
[Development search](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#development-search)  
[Training search](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#training-search)  
[Send to trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#send-to-trade-pile)  
[Quick sell](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#quick-sell)  
[Remove from watch list](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#remove-from-watch-list)  
[Remove from trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#remove-from-trade-pile)  
[Get pile sizes](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-pile-sizes)  
[Relist Tradepile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#relist-tradepile)  
[Get players from club](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-players-from-club)  
[Get squads from club](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-squads-from-club)  
[Get squad details](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-squad-details)  
[Get definitions](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-definitions)  
[Get price ranges](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-price-ranges)  
[Get & Solve Captcha](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-solve-captcha)  
[Remove sold items from trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#remove-sold-items-from-trade-pile)  
[Open a pack](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#open-a-pack)  

### Initialization

```csharp
var client = new FutClient();
```

### Login

```csharp
var loginDetails = new LoginDetails("e-mail", "password", "secret answer", Platform.Ps4 /* or any of the other platforms */, AppVersion.WebApp /* or AppVersion.CompanionApp */);
ITwoFactorCodeProvider provider = // initialize an implementation of this interface
var loginResponse = await client.LoginAsync(loginDetails, provider);
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
### Club image

- Format: PNG
- Dimensions: 256 x 256 pixels

```csharp
var imageBytes = await client.GetClubImageAsync(auctionInfo);
```

### Nation image

- Format: PNG
- Dimensions: 71 x 45 pixels

```csharp
var imageBytes = await client.GetNationImageAsync(auctionInfo);
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

### Relist Tradepile

Re-listing all tradepile items listed before.

```csharp
await client.ReListAsync();
```

### Get players from club

Gets the players from your 'My Club' section.  Note, this will be expanded to include staff and club items.

```csharp
var clubItems = await client.GetClubItemsAsync();
foreach (var itemData in clubItems.ItemData)
{
    // deal with players
}
```  

### Get squads from club

Gets the squads in your club.  Note - many of the fields, such as players etc are not populated here and are in the squad details below.

```csharp
var squadListResponse = await client.GetSquadListAsync();
foreach (var squad in squadListResponse.squad)
{
	string name = squad.squadName;
	// etc.
}
```

### Get squad details

```csharp
var squadDetailsResponse = await client.GetSquadDetailsAsync(squad.id);
foreach (var squadPlayer in squadDetailsResponse.players)
{
	var itemData = squadPlayer.itemData;
	//read properties of players etc.  
	//Positions seem to be set by index number and depend on formation
}
```

### Get definitions

Gets all player cards (Standard, IF, SIF, TOTW,...) based on their Asset ID
```csharp
var playerDefinitions = await client.GetDefinitionsAsync(/* AssetId */);

foreach (ItemData itemData in playerDefinitions.ItemData)
{
    var definitionId = itemData.ResourceId;
    // Contains the Definition ID for i.e. a TOTW card, which you can use to search for this specific card
}
```

### Get price ranges

Gets the EA price range - **You can only use this method right after you get tradepile / watchlist!**
```csharp
var priceRanges = await client.GetPriceRangesAsync(/* List of ItemIds */);

foreach (PriceRange priceRange in priceRanges)
{
    priceRange.MaxPrice = // Maximum BIN
    priceRange.MinPrice = // Minimum Starting BID
}
```

### Get & Solve Captcha

Get Captcha as Base64 encoded image
```csharp
CaptchaResponse captchaImg = await futClient.GetCaptchaAsync();
```

Solve Captcha
```csharp
var CaptchaValidate = await futClient.ValidateCaptchaAsync(/* answer */);
```

### Remove sold items from trade pile

Removes all sold items from the trade pile.

```csharp
await client.RemoveSoldItemsFromTradePileAsync();
```

### Open a pack

Get all available Packs

```csharp
var storeResponse = await futClient.GetPackDetailsAsync();
```

Buy pack

```csharp
// Identify the pack id
uint packId = 0;

foreach (var packDetail in storeResponse.Purchase)
{
    if (packDetail.Coins == 7500)
    {
        packId = packDetail.Id;
    }
}

// Buy Pack
var buyPackResponse = await futClient.BuyPackAsync(new PackDetails(packId));
```

