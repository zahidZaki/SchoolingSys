using System.ComponentModel;

namespace SchoolSysMvc.Dto
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


    public enum CaseStatus
    {
        [Description("Open")]
        Open = 17,
        [Description("Waiting To Accept")]
        WaitingToAccept = 18,
        [Description("Accepted")]
        Accepted = 19,
        [Description("Complete")]
        Complete = 20,
        [Description("Cancelled")]
        Cancelled = 140
    }
    public enum TelepsychStatus
    {
        [Description("Open")]//for uat
        Open = 490,
        [Description("Waiting to Accept")]
        WaitingToAccept = 491,
        [Description("Accepted")]
        Accepted = 492,
        [Description("Completed")]
        Completed = 493,
        [Description("Cancelled")]
        Cancelled = 494,
        [Description("Pending Assignment")]
        PendingAssignment = 652,
        [Description("Suspended")]
        Suspended = 712,
        [Description("Queued")]
        Queued = 750
    }


    public enum CancelReasons
    {
        [Description("Duplicate Case")]
        DuplicateCase = 1,
        [Description("Error Case")]
        ErrorCase = 2
    }
}
