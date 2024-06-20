using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Models.Player
{
    public class Item
    {
        private byte _rating;

        /*
        * Players - Defender
        * ------------------
        * Attribute1 = Pace
        * Attribute2 = Shooting
        * Attribute3 = Passing
        * Attribute4 = Dribbling
        * Attribute5 = Defending
        * Attribute6 = Heading
        */

        public byte Attribute1 { get; set; }

        public byte Attribute2 { get; set; }

        public byte Attribute3 { get; set; }

        public byte Attribute4 { get; set; }

        public byte Attribute5 { get; set; }

        public byte Attribute6 { get; set; }

        public uint ClubId { get; set; }

        public string CommonName { get; set; }

        public DateOfBirth DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public byte Height { get; set; }

        public string ItemType { get; set; }

        public string LastName { get; set; }

        public uint LeagueId { get; set; }

        public uint NationId { get; set; }

        public string PreferredFoot { get; set; }

        public RareType Rare { get; set; }

        public byte Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                SetCardType();
            }
        }

        public CardType CardType { get; set; }

        private void SetCardType()
        {
            if (Rating < 65)
                CardType = CardType.Bronze;
            else if (Rating < 75)
                CardType = CardType.Silver;
        }

        public string Desc { get; set; }

        public uint Bronze { get; set; }

        public uint Silver { get; set; }

        public uint Gold { get; set; }

        public uint Value { get; set; }

        public uint Weight { get; set; }

        public uint FormationId { get; set; }

        public uint TalkRating { get; set; }

        public uint Negotiation { get; set; }

        public uint AssetId { get; set; }

        public uint Attr { get; set; }

        public uint Amount { get; set; }

        public uint AssetYear { get; set; }


        public uint Category { get; set; }

        public string BioDesc { get; set; }

        public string Manufacturer { get; set; }

        public string Name { get; set; }

        public string ResourceGameYear { get; set; }

        public uint StadiumId { get; set; }

        public uint Cap { get; set; }

        public uint Boost { get; set; }

        public uint Pos { get; set; }

        public uint PosBonus { get; set; }

        public string InternationalRep { get; set; }
    }
}
