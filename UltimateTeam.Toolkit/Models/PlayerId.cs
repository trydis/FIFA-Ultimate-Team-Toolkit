namespace UltimateTeam.Toolkit.Models
{
    public class PlayerId
    {
        public long ResourceId { get; set; }

        public long AssetId { get; set; }

        public long DefinitonId { get; set; }

        private long _resourceId;
        private long _assetId;
        private long _definitionId;
        private long _playerId;

        public PlayerId(long Id)
        {
            _playerId = Id;
            _definitionId = 0;

            if (_playerId > 500000)
            {
                _definitionId = _playerId;
            }

            if (_playerId < 1)
            {
                _resourceId = _playerId;
                _assetId = -2147483648 + _resourceId;
            }
            else if (_playerId > 1 && _playerId < 500000)
            {
                _assetId = _playerId;
                _resourceId = -2147483648 + _assetId;
            }

            ResourceId = _resourceId;
            AssetId = _assetId;
            DefinitonId = _definitionId;
            
        }
    }
}