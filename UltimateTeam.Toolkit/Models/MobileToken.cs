namespace UltimateTeam.Toolkit.Models
{
    public class MobileToken
    {
        public string AccessToken { get; set; }
        public string AdditionalAccessTokens { get; set; }
        public string Code { get; set; }
        public string ExpiresIn { get; set; }
        public string IdToken { get; set; }
        public string OriginAccessToken { get; set; }
        public string OriginIdToken { get; set; }
        public string OriginRefreshToken { get; set; }
        public Pid Pid { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
    }
}
