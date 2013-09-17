namespace UltimateTeam.Toolkit.Models
{
    public class Shard
    {
        public string ShardId { get; set; }

        public string ClientFacingIpPort { get; set; }

        public string ClientProtocol { get; set; }

        public string[] Content { get; set; }

        public string[] Platforms { get; set; }

        public string[] CustomData1 { get; set; }
    }
}