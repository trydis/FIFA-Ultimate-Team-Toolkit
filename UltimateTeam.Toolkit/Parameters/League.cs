using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class League : SearchParameterBase<uint>
    {
        public const uint Any = 0;
        public const uint Abundesliga = 80;
        public const uint AirtricityLeague = 65;
        public const uint ALeague = 351;
        public const uint Allsvenskan = 56;
        public const uint BarclaysPremierLeague = 13;
        public const uint Bundesliga = 19;
        public const uint Bundesliga2 = 20;
        public const uint Eredivisie = 10;
        public const uint HellasLiga = 63;
        public const uint KLeague = 83;
        public const uint LigaAdelante = 54;
        public const uint LigaBbva = 53;
        public const uint LigadoBrasil = 7;
        public const uint LigadoBrasilB = 2025;
        public const uint LigaMx = 341;
        public const uint LigaPortuguesa = 308;
        public const uint Ligue1 = 16;
        public const uint Ligue2 = 17;
        public const uint Mls = 39;
        public const uint NpowerChampionship = 14;
        public const uint NpowerLeague1 = 60;
        public const uint NpowerLeague2 = 61;
        public const uint PolskaLiga = 66;
        public const uint ProLeague = 4;
        public const uint RaiffeisenSl = 189;
        public const uint RussianLeague = 67;
        public const uint SaudiProLeague = 350;
        public const uint SerieA = 31;
        public const uint SerieB = 32;
        public const uint SouthAfricanFl = 347;
        public const uint Spl = 50;
        public const uint Superliga = 1;
        public const uint Tippeligaen = 41;
        public const uint TurkLig = 68;

        private League(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<League> GetAll()
        {
            yield return new League("A. Bundesliga", Abundesliga);
            yield return new League("Airtricity League", AirtricityLeague);
            yield return new League("A-League", ALeague);
            yield return new League("Allsvenskan", Allsvenskan);
            yield return new League("Raiffeisen SL", RaiffeisenSl);
            yield return new League("Barclays Premier League", BarclaysPremierLeague);
            yield return new League("Bundesliga", Bundesliga);
            yield return new League("Bundesliga 2", Bundesliga2);
            yield return new League("Eredivisie", Eredivisie);
            yield return new League("Hellas Liga", HellasLiga);
            yield return new League("K-League", KLeague);
            yield return new League("Liga MX", LigaMx);
            yield return new League("Liga Adelante", LigaAdelante);
            yield return new League("Liga BBVA", LigaBbva);
            yield return new League("Liga do Brasil", LigadoBrasil);
            yield return new League("Liga do Brasil B", LigadoBrasilB);
            yield return new League("Liga Portuguesa", LigaPortuguesa);
            yield return new League("Ligue 1", Ligue1);
            yield return new League("Ligue 2", Ligue2);
            yield return new League("MLS", Mls);
            yield return new League("Npower Championship", NpowerChampionship);
            yield return new League("Npower League 1", NpowerLeague1);
            yield return new League("Npower League 2", NpowerLeague2);
            yield return new League("Polska Liga", PolskaLiga);
            yield return new League("Pro League", ProLeague);
            yield return new League("Russian League", RussianLeague);
            yield return new League("Saudi Pro League", SaudiProLeague);
            yield return new League("Serie A", SerieA);
            yield return new League("Serie B", SerieB);
            yield return new League("South African FL", SouthAfricanFl);
            yield return new League("Spl", Spl);
            yield return new League("Superliga", Superliga);
            yield return new League("Tippeligaen", Tippeligaen);
            yield return new League("TurkLig", TurkLig);
        }
    }
}