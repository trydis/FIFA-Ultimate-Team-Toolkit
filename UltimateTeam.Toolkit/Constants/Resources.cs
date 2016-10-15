using System;

namespace UltimateTeam.Toolkit.Constants
{
    internal class Resources
    {
        public string Validate;

        public string Auth;

        public string Home;

        public string NucleusId;

        public string Shards;

        public string AccountInfo;

        public string Item;

        public string PlayerImage;

        public string FlagsImage;

        public string ClubImage;

        public string FutHome = "https://utas.s2.fut.ea.com/ut/game/fifa17/";

        public const string FutHomeXbox = "https://utas.s3.fut.ea.com/ut/game/fifa17/";

        public const string ValidateXbox = "https://utas.s3.fut.ea.com/ut/game/fifa17/phishing/validate";

        public const string AuthXbox = "https://utas.s3.fut.ea.com/ut/auth?timestamp={0}";

        public const string AccountInfoXbox = "https://utas.s3.fut.ea.com/ut/game/fifa17/user/accountinfo?sku=FUT17AND&_={0}";

        public const string POWAuth = "https://pas.mob.v3.easfc.ea.com:8095/pow/auth?timestamp={0}";

        public const string AuthCode = "https://accounts.ea.com/connect/auth";

        public const string CaptchaImage = "https://www.easports.com/iframe/fut17/p/ut/captcha/img?token=AAAA&_={0}";

        public const string CaptchaValidate = "https://www.easports.com/iframe/fut17/p/ut/captcha/validate";

        public const string Token = "https://accounts.ea.com/connect/token";

        public const string Pid = "https://gateway.ea.com/proxy/identity/pids/me";

        public const string LoggedIn = "https://www.easports.com/fifa/api/isUserLoggedIn";

        public const string BaseShowoff = "http://www.easports.com/iframe/fut17/?baseShowoffUrl=https%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Fultimate-team%2Fweb-app%2Fshow-off&guest_app_uri=http%3A%2F%2Fwww.easports.com%2Fuk%2Ffifa%2Fultimate-team%2Fweb-app&locale=en_GB";

        public const string PriceRange = "marketdata/item/pricelimits?itemIdList={0}";

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


        public Resources(AppVersion appVersion)
        {
            switch (appVersion)
            {
                case AppVersion.WebApp:

                    Validate = "https://www.easports.com/iframe/fut17/p/ut/game/fifa17/phishing/validate";

                    Auth = "https://www.easports.com/iframe/fut17/p/ut/auth";

                    Home = "https://www.easports.com/fifa/ultimate-team/web-app";

                    NucleusId = "https://www.easports.com/iframe/fut17/?locale=en_US&baseShowoffUrl=https%3A%2F%2Fwww.easports.com%2Ffifa%2Fultimate-team%2Fweb-app%2Fshow-off&guest_app_uri=http%3A%2F%2Fwww.easports.com%2Ffifa%2Fultimate-team%2Fweb-app";

                    Shards = "https://www.easports.com/iframe/fut17/p/ut/shards/v2?_={0}";

                    AccountInfo = "https://www.easports.com/iframe/fut17/p/ut/game/fifa17/user/accountinfo?sku=FUT17WEB&_={0}";

                    Item = "https://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/web/{0}.json";

                    PlayerImage = "http://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/images/players/web/{0}.png";

                    FlagsImage = "http://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/images/cardflagssmall/web/{0}.png";

                    ClubImage = "http://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/images/clubbadges/web/dark/s{0}.png";

                    break;

                case AppVersion.CompanionApp:

                    Validate = "https://utas.s2.fut.ea.com/ut/game/fifa17/phishing/validate";

                    Auth = "https://utas.s2.fut.ea.com/ut/auth?timestamp={0}";

                    Home = "https://accounts.ea.com/connect/auth?client_id=FIFA-17-MOBILE-COMPANION&response_type=code&display=web2/login&scope=basic.identity+offline+signin&locale=en_GB&prompt=login&machineProfileKey={0}";

                    NucleusId = "https://pas.mob.v3.easfc.ea.com:8095/pow/user/self/tiergp/NucleusId/tiertp/{0}?offset=0&count=50&_={1}";

                    Shards = "https://utas.mob.v3.fut.ea.com/ut/shards/v2?_={0}";

                    AccountInfo = "https://utas.s2.fut.ea.com/ut/game/fifa17/user/accountinfo?sku=FUT17AND&_={0}";

                    Item = "https://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/web/{0}.json";

                    PlayerImage = "http://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/images/players/web/{0}.png";

                    FlagsImage = "http://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/images/cardflagssmall/web/{0}.png";

                    ClubImage = "http://fifa17.content.easports.com/fifa/fltOnlineAssets/B488919F-23B5-497F-9FC0-CACFB38863D0/2017/fut/items/images/clubbadges/web/dark/s{0}.png";

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(appVersion), appVersion, null);
            }
        }
    }
}
