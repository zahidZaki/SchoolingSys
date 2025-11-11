namespace SchoolingSysApiGateway.Dto
{
    public class EnumCollection
    {
    }
    public enum ApiResponseStatus
    {
        Success = 0,
        Failure = -1,
        ServerError = -2,
        ValidationError = 3
    }
    public enum UserStatusEnum
    {
        Available = 1,
        NotAvailable = 2,
        OnATicket = 3
    }
}
