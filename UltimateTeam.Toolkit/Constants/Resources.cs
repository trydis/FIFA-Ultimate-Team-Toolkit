using System;

namespace UltimateTeam.Toolkit.Constants
{
    internal class Resources
    {
        private const int CurrentYearShort = 20;

        private const int CurrentYearLong = 2020;

        private const int PreviousYearShort = CurrentYearShort - 1;

        private const int PreviousYearLong = CurrentYearLong - 1;

        public int ClientVersion;

        public string Home;

        public string Logout;

        public string FutHome;

        public string ValidateQuestion;

        public string ValidateAnswer;

        public string Auth;

        public string Shards;

        public string AccountInfo;

        public string Item;

        public string PlayerImage;

        public string FlagsImage;

        public string ClubImage;

        public string AuthCode;

        public string Token;

        public string Pid;

        public const string Store = "store/purchaseGroup/cardpack?ppInfo=true";

        public const string MyClub = "club?level=10";

        public const string TransferMarket = "transfermarket";

        public const string Bid = "trade/{0}/bid";

        public const string TradeStatus = "trade/status?tradeIds={0}";

        public const string Credits = "user/credits";

        public const string Auctionhouse = "auctionhouse";

        public const string TradePile = "tradepile";

        public const string Watchlist = "watchlist";

        public const string PurchasedItems = "purchased/items";

        public const string ListItem = "item";

        public const string QuickSell = "item?itemIds={0}";

        public const string RemoveFromTradePile = "trade/{0}";

        public const string PileSize = "clientdata/pileSize";

        public const string Consumables = "club/stats/consumables";

        public const string SquadList = "squad/list";

        public const string SquadDetails = "squad/{0}";

        public const string ReList = "/relist";

        public const string ActiveMessageList = "activeMessage";

        public const string ActiveMessageGet = "activeMessage/{0}";

        public const string Definition = "defid?type=player&count=35&start=0&defId={0}";

        public const string Settings = "settings";

        public const string ReturningUser = "user";

        public string Sku = "";

        public string Client = "";

        public string GameSku = $"FFA{CurrentYearShort}";


        public Resources(AppVersion appVersion)
        {
            FutHome = $"https://utas.external.s2.fut.ea.com/ut/game/fifa{CurrentYearShort}/";

            ValidateQuestion = $"https://utas.external.s2.fut.ea.com/ut/game/fifa{CurrentYearShort}/phishing/question?_=" + "{0}";

            ValidateAnswer = $"https://utas.external.s2.fut.ea.com/ut/game/fifa{CurrentYearShort}/phishing/validate?answer=" + "{0}";

            Auth = "https://utas.external.s2.fut.ea.com/ut/auth?" + "{0}";

            Shards = "https://utas.mob.v4.fut.ea.com/ut/shards/v2?_=" + "{0}";

            AccountInfo = $"https://utas.external.s2.fut.ea.com/ut/game/fifa{CurrentYearShort}/user/accountinfo?filterConsoleLogin=true&sku={Sku}&returningUserGameYear={PreviousYearLong}&_=" + "{0}";

            AuthCode = "https://accounts.ea.com/connect/auth?client_id=FOS-SERVER&redirect_uri=nucleus:rest&response_type=code&access_token=" + "{0}";

            Token = "https://accounts.ea.com/connect/token";

            Pid = "https://gateway.ea.com/proxy/identity/pids/me";

            Item = "http://fifa.content.easports.com/fifa/fltOnlineAssets/CC8267B6-0817-4842-BB6A-A20F88B05418/2017/fut/items/web/{0}.json";

            PlayerImage = "http://fifa.content.easports.com/fifa/fltOnlineAssets/CC8267B6-0817-4842-BB6A-A20F88B05418/2017/fut/items/images/players/web/{0}.png";

            FlagsImage = "http://fifa.content.easports.com/fifa/fltOnlineAssets/CC8267B6-0817-4842-BB6A-A20F88B05418/2017/fut/items/images/cardflagssmall/web/{0}.png";

            ClubImage = "http://fifa.content.easports.com/fifa/fltOnlineAssets/CC8267B6-0817-4842-BB6A-A20F88B05418/2017/fut/items/images/clubbadges/web/dark/s{0}.png";

            switch (appVersion)
            {
                case AppVersion.WebApp:

                    Sku = $"FUT{CurrentYearShort}WEB";
                    Client = $"FIFA-{CurrentYearShort}-WEBCLIENT";

                    ClientVersion = 1;

                    Home = $"https://accounts.ea.com/connect/auth?prompt=login&accessToken=null&client_id={Client}&response_type=token&display=web2/login&locale=en_GB&redirect_uri=https://www.easports.com/de/fifa/ultimate-team/web-app/auth.html&scope=basic.identity+offline+signin";

                    Logout = $"https://accounts.ea.com/connect/logout?client_id={Client}&redirect_uri=nucleus:rest";

                    break;

                case AppVersion.CompanionApp:

                    Sku = $"FUT{CurrentYearShort}AND";
                    Client = $"FIFA-{CurrentYearShort}-MOBILE-COMPANION";

                    ClientVersion = 21;

  
                    Home = $"https://accounts.ea.com/connect/auth?client_id={Client}&response_type=token&display=web2/login&locale=en_GB&redirect_uri=nucleus:rest&prompt=none&scope=basic.identity+offline+signin";

                    Logout = $"https://accounts.ea.com/connect/logout?client_id={Client}&redirect_uri=nucleus:rest";

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(appVersion), appVersion, null);
            }
        }
    }
}
