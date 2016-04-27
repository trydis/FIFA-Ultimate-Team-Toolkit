using System;

namespace UltimateTeam.Toolkit.Models
{
    public class PackDetails
    {
        public Constants.Currency Currency { get; set; }

        public uint PackId { get; set; }

        public uint UseCredits { get; set; }

        public bool UsePreOrder { get; set; }

        public PackDetails(uint packId, Constants.Currency currency = Constants.Currency.COINS, uint useCredits = 0, bool usePreOrder = false)
        {
            if (packId <= 0) throw new ArgumentException("Invalid Pack Id");

            Currency = currency;
            PackId = packId;
            UseCredits = useCredits;
            UsePreOrder = usePreOrder;
        }
    }
}
