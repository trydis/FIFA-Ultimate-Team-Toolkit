using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class League : SearchParameterBase<uint>
    {
        public const uint Any = 0;
        public const uint AlkaSuperliga = 1;
        public const uint Allsvenskan = 56;
        public const uint Bundesliga = 19;
        public const uint Bundesliga2 = 20;
        public const uint CalcioA = 31;
        public const uint CalcioB = 32;
        public const uint CampScotiabank = 335;
        public const uint DawryJameel = 350;
        public const uint DominosLigue2 = 17;
        public const uint EFLChampionship = 14;
        public const uint EFLLeagueOne = 60;
        public const uint EFLLeagueTwo = 61;
        public const uint Ekstraklasa = 66;
        public const uint Eredivisie = 10;
        public const uint Finnliiga = 322;
        public const uint HellasLiga = 63;
        public const uint HyundaiALeague = 351;
        public const uint KLeagueClassic = 83;
        public const uint LaLiga123 = 54;
        public const uint LaLigaSantander = 53;
        public const uint Legends = 2118;
        public const uint LIGABancomerMX = 341;
        public const uint LigaDimayor = 336;
        public const uint LigaNOS = 308;
        public const uint Ligue1 = 16;
        public const uint MLS = 39;
        public const uint MeijiYasudaJ1League = 349;
        public const uint OBundesliga = 80;
        public const uint PremierLeague = 13;
        public const uint PrimeraDivision = 353;
        public const uint ProLeague = 4;
        public const uint RSL = 189;
        public const uint RussianLeague = 67;
        public const uint ScottishPremiership = 50;
        public const uint SouthAfricanFL = 347;
        public const uint SSEAirtricityLeague = 65;
        public const uint SuperLig = 68;
        public const uint Tippeligaen = 41;
        public const uint UkrayinaLiha = 332;
        public const uint UltimateTeam = 2134;

        private League(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<League> GetAll()
        {
            yield return new League("Alka Superliga", AlkaSuperliga);
            yield return new League("Allsvenskan", Allsvenskan);
            yield return new League("Bundesliga", Bundesliga);
            yield return new League("Bundesliga 2", Bundesliga2);
            yield return new League("Calcio A", CalcioA);
            yield return new League("Calcio B", CalcioB);
            yield return new League("Camp. Scotiabank", CampScotiabank);
            yield return new League("Dawry Jameel", DawryJameel);
            yield return new League("Domino's Ligue 2", DominosLigue2);
            yield return new League("EFL Championship", EFLChampionship);
            yield return new League("EFL League One", EFLLeagueOne);
            yield return new League("EFL League Two", EFLLeagueTwo);
            yield return new League("T-Mobile Ekstraklasa", Ekstraklasa);
            yield return new League("Eredivisie", Eredivisie);
            yield return new League("Finnliiga", Finnliiga);
            yield return new League("Hellas Liga", HellasLiga);
            yield return new League("Hyundai A-League", HyundaiALeague);
            yield return new League("K LEAGUE Classic", KLeagueClassic);
            yield return new League("LaLiga 1 | 2 | 3", LaLiga123);
            yield return new League("LaLiga Santander", LaLigaSantander);
            yield return new League("Legends", Legends);
            yield return new League("LIGA Bancomer MX", LIGABancomerMX);
            yield return new League("Liga Dimayor", LigaDimayor);
            yield return new League("Liga NOS", LigaNOS);
            yield return new League("Ligue 1", Ligue1);
            yield return new League("Major League Soccer", MLS);
            yield return new League("Meiji Yasuda J1 League", MeijiYasudaJ1League);
            yield return new League("Osterreichische Fussball-Bundesliga", OBundesliga);
            yield return new League("Premier League", PremierLeague);
            yield return new League("Primera Division", PrimeraDivision);
            yield return new League("Belgium Pro League", ProLeague);
            yield return new League("Raiffeisen Super League", RSL);
            yield return new League("Russian League", RussianLeague);
            yield return new League("Scottish Premiership", ScottishPremiership);
            yield return new League("South African FL", SouthAfricanFL);
            yield return new League("SSE Airtricity League", SSEAirtricityLeague);
            yield return new League("Super Lig", SuperLig);
            yield return new League("Tippeligaen", Tippeligaen);
            yield return new League("Ukrayina Liha", UkrayinaLiha);
            yield return new League("Ultimate Team", UltimateTeam);
        }
    }
}
