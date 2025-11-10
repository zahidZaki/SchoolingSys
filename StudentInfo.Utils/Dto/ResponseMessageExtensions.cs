using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Utils.Dto
{
    public static class ResponseMessageExtensions
    {
        public enum ResponseMessages
        {
            SaveSuccess,
            UpdateSuccess,
            DeleteSuccess,
            ErrorOccurred,
            NotFound,
            Found,
            InvalidCredentials,
            InActive,
            UserFound,
            Success,
            UserSuccess,
            UserFailed,
            DataInUse
        }
        public static string GetMessage(this ResponseMessages message)
        {
            return message switch
            {
                ResponseMessages.SaveSuccess => "Created Successfully.",
                ResponseMessages.UpdateSuccess => "Updated Successfully.",
                ResponseMessages.DeleteSuccess => "Deleted Successfully.",
                ResponseMessages.ErrorOccurred => "An error occurred. Please try again.",
                ResponseMessages.NotFound => "Record Not found.",
                ResponseMessages.Found => "Record Found.",
                ResponseMessages.InvalidCredentials => "Invalid Username/Password",
                ResponseMessages.InActive => "User is InActive",
                ResponseMessages.UserFound => "User Found",
                ResponseMessages.Success => "success",
                ResponseMessages.UserSuccess => "User has been Added Successfully",
                ResponseMessages.UserFailed => "User has not been Added Successfully!",
                ResponseMessages.DataInUse => "The requested data is currently in use elsewhere and cannot be modified.",
                _ => throw new ArgumentOutOfRangeException(nameof(message), message, null)
            };
        }
    }
}
