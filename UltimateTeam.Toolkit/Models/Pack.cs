using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class Pack
    {
        public string ActionType { get; set; }

        public uint AssetId { get; set; }

        public uint Coins { get; set; }

        public List<Currency> Currencies { get; set; }

        public string CustomAttribute { get; set; }

        public string DealType { get; set; }

        public string Description { get; set; }

        public PackDisplayGroup DisplayGroup { get; set; }

        public string DisplayGroupAssetId { get; set; }

        public bool DisplayGroupUseDefaultImage { get; set; }

        public PackExt ExtPrice { get; set; }

        public uint FifaCashPrice { get; set; }

        public string FirstPartyStoreId { get; set; }

        public uint Id { get; set; }

        public bool IsPremium { get; set; }

        public bool IsSeasonTicketDiscount { get; set; }

        public string LastPurchasedTime { get; set; }

        public PackContentInfo PackContentInfo { get; set; }

        public string PackType { get; set; }

        public string ProductId { get; set; }

        public uint PurchaseCount { get; set; }

        public uint PurchaseLimit { get; set; }

        public string PurchaseMethod { get; set; }

        public int Quantity { get; set; }

        public uint SaleId { get; set; }

        public string SaleType { get; set; }

        public uint SortPriority { get; set; }

        public long SecondsUntilStart { get; set; }

        public long SecondsUntilEnd { get; set; }

        public long Start { get; set; }

        public long End { get; set; }

        public string State { get; set; }

        public string Type { get; set; }

        public bool Unopened { get; set; }

        public bool UseDefaultImage { get; set; }
    }
}
