using System;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Constants
{
    /// <summary>
    /// Enumerating Pin Event Ids
    /// </summary>
    [DataContract(Name = "PinEventIds")]
    public enum PinEventId
    {
        [EnumMember(Value = "Home")]
        WebApp_Home,
        [EnumMember(Value = "Login")]
        WebApp_Login,
        [EnumMember(Value = "TOTW")]
        TOTW,
        [EnumMember(Value = "Generations")]
        WebApp_Generations,
        [EnumMember(Value = "Unassigned Items")]
        WebApp_UnassignedItems,
        [EnumMember(Value = "Transfer Targets")]
        WebApp_TransferTargets,
        [EnumMember(Value = "Transfer List")]
        WebApp_TransferList,
        [EnumMember(Value = "Transfer Market Search Results")]
        WebApp_TransferMarketSearchResults,
        [EnumMember(Value = "Club - Players")]
        WebApp_Players,
        [EnumMember(Value = "Club - Staff")]
        WebApp_Staff,
        [EnumMember(Value = "Club - Items")]
        WebApp_Items,
        [EnumMember(Value = "Club - Consumables")]
        WebApp_Consumables,
        [EnumMember(Value = "Leaderboards - Main")]
        WebApp_Leaderboards,
        [EnumMember(Value = "Leaderboards - Match Earnings")]
        WebApp_LeaderboardsEarnings,
        [EnumMember(Value = "Leaderboards - Transfer Profit")]
        WebApp_LeaderboardsTransferProfit,
        [EnumMember(Value = "Leaderboards - Club Value")]
        WebApp_LeaderboardsClubValue,
        [EnumMember(Value = "Leaderboards - Top Squad")]
        WebApp_LeaderboardsTopSquad,
        [EnumMember(Value = "Store - Main")]
        WebApp_MainStore,
        [EnumMember(Value = "Store - Gold")]
        WebApp_GoldStore,
        [EnumMember(Value = "Store - Silver")]
        WebApp_SilverStore,
        [EnumMember(Value = "Store - Bronze")]
        WebApp_BronzeStore,

        [EnumMember(Value = "Companion App - OPEN")]
        CompanionApp_AppOpened,
        [EnumMember(Value = "Companion App - CONNECT")]
        CompanionApp_Connect,
        [EnumMember(Value = "Companion App - CONNECTED")]
        CompanionApp_Connected,
        [EnumMember(Value = "Hub - Home")]
        CompanionApp_Home,
        [EnumMember(Value = "Hub - Squads")]
        CompanionApp_HubSquads,
        [EnumMember(Value = "Hub - Draft")]
        CompanionApp_HubDraft,
        [EnumMember(Value = "Hub - Transfers")]
        CompanionApp_HubTransfers,
        [EnumMember(Value = "Hub - Store")]
        CompanionApp_HubStore,
        [EnumMember(Value = "Hub - Unassigned")]
        CompanionApp_HubUnassigned,
        [EnumMember(Value = "Unassigned Items - List View")]
        CompanionApp_UnassignedItems,
        [EnumMember(Value = "Unassigned Items - Detail View")]
        CompanionApp_UnassignedItems_Detailed,
        [EnumMember(Value = "Transfer Targets - List View")]
        CompanionApp_TransferTargets,
        [EnumMember(Value = "Transfer Targets - Detail View")]
        CompanionApp_TransferTargets_Detailed,
        [EnumMember(Value = "Transfer List - List View")]
        CompanionApp_TransferList,
        [EnumMember(Value = "Transfer List - Detail View")]
        CompanionApp_TransferList_Detailed,
        [EnumMember(Value = "Transfer Market Results - List View")]
        CompanionApp_TransferMarketResults,
        [EnumMember(Value = "Transfer Market Results - Detail View")]
        CompanionApp_TransferMarketResults_Detailed,
        [EnumMember(Value = "Active Squad - Details")]
        CompanionApp_ActiveSquad_Details,
        [EnumMember(Value = "Active Squad - Swap Player")]
        CompanionAppp_ActiveSquad_SwapPlayer,
        [EnumMember(Value = "Store - Pack Category")]
        WebApp_PackCategory,
        [EnumMember(Value = "Store - Pack Details")]
        WebApp_PackDetails,
        [EnumMember(Value = "Club - Players - List View")]
        CompanionApp_Players,
        [EnumMember(Value = "Club - Players - Detail View")]
        CompanionApp_Players_Detailed,
        [EnumMember(Value = "Club - Staff - List View")]
        CompanionApp_Staff,
        [EnumMember(Value = "Club - Staff - Detail View")]
        CompanionApp_Staff_Detailed,
        [EnumMember(Value = "Club - Club Items - List View")]
        CompanionApp_Club,
        [EnumMember(Value = "Club - Club Consumables - List View")]
        CompanionApp_Consumables,
        [EnumMember(Value = "Club - Club - Detail View")]
        CompanionApp_Club_Detailed,
        [EnumMember(Value = "EASFC - News")]
        CompanionApp_EASFC_News,
        [EnumMember(Value = "EASFC - News - Preview Squad")]
        CompanionApp_EASFC_PreviewSquad,
        [EnumMember(Value = "Transfer Market Search")]
        Generic_TransferMarketSearch,
        [EnumMember(Value = "Squad List - My Squads")]
        Generic_SquadList,
        [EnumMember(Value = "Squad List - Concept Squads")]
        Generic_ConceptSquadList,
        [EnumMember(Value = "Active Squad")]
        Generic_ActiveSquad,
    }
}