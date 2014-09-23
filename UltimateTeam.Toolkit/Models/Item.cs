namespace UltimateTeam.Toolkit.Models
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
    }
}
