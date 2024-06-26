EAFC Ultimate Team Toolkit
===============================

## Sample usage

[Initialization](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#initialization)  
[Login](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#login)  
[Players list](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#players-list)  
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
[Get watch list](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-watch-list)  
[Get Consumables](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-consumables)  
[Add auction to watch list](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#add-auction-to-watch-list)  
[Get Purchased items](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-purchased-items)  
[Development search](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#development-search)  
[Training search](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#training-search)  
[Send to trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#send-to-trade-pile)  
[Send to club](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#send-to-club)    
[Quick sell](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#quick-sell)  
[Remove from watch list](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#remove-from-watch-list)  
[Remove from trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#remove-from-trade-pile)  
[Get pile sizes](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-pile-sizes)  
[Relist Tradepile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#relist-tradepile)  
[Get players from club](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-players-from-club)  
[Get squads from club](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-squads-from-club)  
[Get squad details](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-squad-details)  
[Get definitions](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#get-definitions)    
[Remove sold items from trade pile](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#remove-sold-items-from-trade-pile)  
[Open a pack](https://github.com/trydis/FIFA-Ultimate-Team-Toolkit#open-a-pack)  

### Initialization

```csharp
var client = new FutClient();
```

### Login

```csharp
var loginDetails = new LoginDetails("e-mail", "password", Platform.Ps5 /* or any of the other platforms */, AppVersion.WebApp /* or AppVersion.CompanionApp not implemented */);
ITwoFactorCodeProvider provider = // initialize an implementation of this interface
var loginResponse = await client.LoginAsync(loginDetails, provider);
```

Example implementation of ITwoFactorCodeProvider interface
```csharp

    ITwoFactorCodeProvider provider = new FutAuth();

    public class FutAuth : ITwoFactorCodeProvider
    {
        public TaskCompletionSource<string> taskResult = new TaskCompletionSource<string>();
        public Task<string> GetTwoFactorCodeAsync(AuthenticationType authType)
        {
            Console.WriteLine($"{ DateTime.Now } Enter OTP ({ authType }):");
            taskResult.SetResult(Console.ReadLine());
            return taskResult.Task;
        }
    }
```

In order to avoid to enter OTP at each session you can overload it with a `CookieHandler`
```csharp
FutClient client = new FutClient(cookieContainer);
```

### Players list

Retrieves all legends and players basic data with Firstname, Lastname, Rating and AssetId (needed for search requests)

```csharp
var playerListResponse = await client.GetPlayerListAsync();
foreach (var player in playerListResponse.Players ) //or playerListResponse.LegendPlayers
{
	// Handle player Data
}
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

It is also possible to search for a definition (i.e. a Herocard of a player)

```csharp
var searchParameters = new PlayerSearchParameters
{
    Page = 1,
    ResourceId = <AssetId> or <ResourceId>,
    MaxBuy = 2500
};

var searchResponse = await client.SearchAsync(searchParameters);
foreach (var auctionInfo in searchResponse.AuctionInfo)
{
	// Handle auction data
}
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

### Place bid

Passing the amount explicitly:

```csharp
var auctionResponse = await client.PlaceBidAsync(auctionInfo, 150);
```

Place the next valid bid amount:

```csharp
var auctionResponse = await client.PlaceBidAsync(auctionInfo);
```

BuyNow:

```csharp
var auctionResponse = await client.PlaceBidAsync(auctionResponse.AuctionInfo[0], auctionResponse.AuctionInfo[0].BuyNowPrice);
```

### Player definition

Gets all player cards (Base, TOTW, TOTS, Hero, etc.) based on their Asset ID

```csharp
var playerDefinitions = await client.GetDefinitionsAsync(/* AssetId */);

foreach (ItemData itemData in playerDefinitions.ItemData)
{
	// Contains the Definition ID for i.e. a TOTW card, which you can use to search for this specific card
	var definitionId = itemData.ResourceId;
}
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

### Get watch list

Retrieves the the watch list.

```csharp
var watchlistResponse = await client.GetWatchlistAsync();
```

### Get Consumables

Retrieves the consumables of your club

```csharp
var consumablesResponse = await client.GetConsumablesAsync();
```

### Add auction to watch list


```csharp
var addAuctionToWatchlistResponse = await client.AddToWatchlistRequestAsync(auctionInfo);
```

### Get purchased items

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
var sendToTradePileResponse = await client.SendToTradePileAsync(itemData);
```
```csharp
var sendToClubResponse = await client.SendToTradePileAsync(IEnumerable<long>); (ItemIds)
```

### Send to club

Sends an item to your club

```csharp
var sendToClubResponse = client.SendToClubAsync(auctionInfo.ItemData, auctionInfo)
```
```csharp
var sendToClubResponse = await client.SendToClubAsync(IEnumerable<long>); (ItemIds)
```

### Quick sell

Quick sell an item at discard value.

```csharp
var quickSellResponse = await client.QuickSellItemAsync(IEnumerable<long>); (ItemIds)
```

### Remove from watch list

Removes an auction from the watch list.

```csharp
await client.RemoveFromWatchlistAsync(IEnumerable<auctionInfo>);
```

### Remove from trade pile

Removes an auction from the trade pile.

```csharp
await client.RemoveFromTradePileAsync(IEnumerable<auctionInfo>);
```

### Remove sold items from trade pile

Removes all sold items from the trade pile.

```csharp
await client.RemoveSoldItemsFromTradePileAsync();
```

### Relist Tradepile

Relists all tradepile items as listed before.

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
foreach (var squad in squadListResponse.squads)
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
	var squadDetailedResponse = await client.GetSquadDetailsAsync((ushort)squad.Id);
}
```

### Open a pack

Get all available Packs

```csharp
var storeResponse = await futClient.GetPackDetailsAsync();
```

Buy pack

```csharp
// Identify the pack Id
var storeResponse = await client.GetPackDetailsAsync();
foreach (var pack in storeResponse.Packs)
{
	int packId = storeResponse.Packs.Where(p => p.Coins < 1000).FirstOrDefault();
}

// Buy Pack
var buyPackResponse = await client.BuyPackAsync((int)cheapestPackResponse.Id, CurrencyOption.COINS);
```
