using UltimateTeam.Toolkit.Constants;

namespace UltimateTeam.Toolkit.Models
{
    public class FutError
    {
        public string Reason { get; set; }

        public FutErrorCode Code { get; set; }

        public string Message { get; set; }

        public string Debug { get; set; }

        public string String { get; set; }
    }
}
