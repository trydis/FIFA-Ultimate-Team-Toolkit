namespace UltimateTeam.Toolkit.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(string errorCode, string description) : base($"HTTP Error: {errorCode} - Description: {description}")
        { }
    }
}