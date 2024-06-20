namespace UltimateTeam.Toolkit.Constants
{
    internal class Resources
    {
        public const int CurrentYearShort = 24;

        public const int CurrentYearLong = 2024;

        public const int PreviousYearShort = CurrentYearShort - 1;

        public const int PreviousYearLong = CurrentYearLong - 1;

        public string Home;

        public string UserAccounts;

        public string Logout;

        public string FutHome;
        public string FutHomeDelete;

        public string Auth;

        public string PlayerImage;

        public string FlagsImage;

        public string ClubImage;

        public string LeagueImage;

        public string AuthCode;

        public string Token;

        public string Pids;

        public const string Store = "store/purchaseGroup/all?ppInfo=true&categoryInfo=true";

        public const string MyClub = "club?level=10";

        public const string TransferMarket = "transfermarket";

        public const string Bid = "trade/{0}/bid";

        public const string TradeStatus = "trade/status/lite?tradeIds={0}";

        public const string Credits = "user/credits";

        public const string Auctionhouse = "auctionhouse";

        public const string TradePile = "tradepile";

        public const string Watchlist = "watchlist";

        public const string PurchasedItems = "purchased/items";

        public const string Item = "item";
        public const string ItemDetails = "item/{0}";

        public const string RemoveFromTradePile = "trade/{0}";

        public const string Consumables = "club/consumables/development";

        public const string SquadList = "squad/list";

        public const string SquadDetails = "squad/{0}";

        public const string ReList = "/relist";

        public const string ActiveMessageList = "activeMessage";

        public const string ActiveMessageGet = "activeMessage/{0}";

        public const string Definition = "defid?count=21&sort=desc&start=0&type=player&defId={0}";

        public const string Settings = "settings";

        public const string ReturningUser = "user";

        public string Sku;
        public string GameSku = $"FFA{CurrentYearShort}";

        public int ClientVersion;
        public string Client;


        public Resources(AppVersion appVersion)
        {
            switch (appVersion)
            {
                case AppVersion.WebApp:
                    ClientVersion = 1;
                    Sku = $"FUT{CurrentYearShort}WEB";
                    Client = $"FC{CurrentYearShort}_JS_WEB_APP";
                    break;

                case AppVersion.CompanionApp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(appVersion), appVersion, null);
            }

            FutHome = $"https://utas.mob.v2.prd.futc-ext.gcp.ea.com/ut/game/fc{CurrentYearShort}/";
            FutHomeDelete = $"https://utas.mob.v2.prd.futc-ext.gcp.ea.com/ut/delete/game/fc{CurrentYearShort}/";
            Home = $"https://accounts.ea.com/connect/auth?hide_create=true&" +
                    "display=web2%2Flogin&scope=basic.identity+offline+signin+basic.entitlement+basic.persona&" +
                    "release_type=prod&response_type=token&" +
                    "redirect_uri=https%3A%2F%2Fwww.ea.com%2Fea-sports-fc%2Fultimate-team%2Fweb-app%2Fauth.html&accessToken=&" +
                    "locale=en_US&prompt=login&" +
                    $"client_id={Client}";
            Auth = "https://utas.mob.v2.prd.futc-ext.gcp.ea.com/ut/auth";
            AuthCode = "https://accounts.ea.com/connect/auth?client_id=FUTWEB_BK_OL_SERVER&" +
                "redirect_uri=nucleus:rest&response_type=code&access_token={0}&" +
                "release_type=prod&client_sequence={1}";
            UserAccounts = $"https://utas.mob.v2.prd.futc-ext.gcp.ea.com/ut/game/fc{CurrentYearShort}/v2/user/accountinfo?filterConsoleLogin=true&sku={Sku}&returningUserGameYear={PreviousYearLong}&clientVersion={ClientVersion}";
            Pids = "https://gateway.ea.com/proxy/identity/pids/me";

            //not tested
            Logout = $"https://accounts.ea.com/connect/logout?client_id={Client}&redirect_uri=nucleus:rest";

            PlayerImage = $"https://www.ea.com/ea-sports-fc/ultimate-team/web-app/content/24B23FDE-7835-41C2-87A2-F453DFDB2E82/{CurrentYearLong}/fut/items/images/mobile/portraits/{0}.png";
            FlagsImage = $"https://www.ea.com/ea-sports-fc/ultimate-team/web-app/content/24B23FDE-7835-41C2-87A2-F453DFDB2E82/{CurrentYearLong}/fut/items/images/mobile/flags/card/{0}.png";
            ClubImage = $"https://www.ea.com/ea-sports-fc/ultimate-team/web-app/content/24B23FDE-7835-41C2-87A2-F453DFDB2E82/{CurrentYearLong}/fut/items/images/mobile/clubs/normal/{0}.png";
            LeagueImage = $"https://www.ea.com/ea-sports-fc/ultimate-team/web-app/content/24B23FDE-7835-41C2-87A2-F453DFDB2E82/{CurrentYearLong}/fut/items/images/mobile/leagueLogos/light/{0}.png";
        }
    }
}