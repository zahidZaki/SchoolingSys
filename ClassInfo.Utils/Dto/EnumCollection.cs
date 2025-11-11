using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInfo.Utils.Dto
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
    public enum UserModuleName
    {
        FREE = 1,
        HD = 2,
        TI = 3
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

    public enum TaskOrigin
    {
        Board = 1,
        Assigned = 2,
        Acknowledge = 3,
        BoardAndAssigned = 4,
        IsNoticed = 5,
        Escalate = 6
    }

    public enum CancelReasons
    {
        [Description("Duplicate Case")]
        DuplicateCase = 1,
        [Description("Error Case")]
        ErrorCase = 2
    }



    public enum RRCType
    {
        [Description("Possible Duplicate stroke")]
        PossibleDuplicateStroke = 1,

        [Description("Lack of Patient Demographics in e-alert")]
        LackOfPatientDemographicsInEAlert = 2,

        [Description("No video on after accepting case after 5 minutes")]
        NoVideoAfterAcceptingCaseAfter5Minutes = 3,

        [Description("Accepted Case video log in time > 10")]
        AcceptedCaseVideoLogInTimeGreaterThan10 = 4,

        [Description("Verify ETA")]
        VerifyETA = 5,

        [Description("No one is at the cart")]
        NoOneAtTheCart = 6,

        [Description("Cart is not by patient bedside")]
        CartNotByPatientBedside = 7,

        [Description("Patient is not in room")]
        PatientNotInRoom = 8,

        [Description("Patient is leaving room for testing or other")]
        PatientLeavingRoomForTestingOrOther = 9,

        [Description("Cart is by wrong patient")]
        CartByWrongPatient = 10,

        [Description("Nurse not in the room")]
        NurseNotInTheRoom = 11,

        [Description("Case is cancelled")]
        CaseCancelled = 12,

        [Description("The on ground neurologist is evaluating the patient")]
        OnGroundNeurologistEvaluatingPatient = 13,

        [Description("I am ready to see the patient. Please have the cart and Nurse ready at bedside.")]
        ReadyToSeePatientCartAndNurseReadyAtBedside = 14,

        [Description("Please change the template from reassessment to psych evaluation")]
        ChangeTemplateToPsychEvaluation = 15,

        [Description("Patient has not arrived")]
        PatientHasNotArrived = 16,

        [Description("Intake STAT call")]
        IntakeSTATCall = 17,

        [Description("Telepsych: 2 Hours Check")]
        Telepsych2HoursCheck = 18
    }
}
