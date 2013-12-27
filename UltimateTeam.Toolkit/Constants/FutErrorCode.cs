namespace UltimateTeam.Toolkit.Constants
{
    public enum FutErrorCode : ushort
    {
        ExpiredSession = 401,
        NotFound = 404,
        Conflict = 409,
        BadRequest = 460,
        PermissionDenied = 461,
        NotEnoughCredit = 470,
        NoSuchTradeExists = 478,
        InternalServerError = 500,
        ServiceUnavailable = 503
    }
}
