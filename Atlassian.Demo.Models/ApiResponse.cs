using Atlassian.Demo.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Atlassian.Demo.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Errors = new List<Error>();
        }

        public T Data { get; set; }

        public IList<Error> Errors { get; set; }
    }

    public class Error
    {
        public Error()
        {
        }


        public Error(int code, string message, string description)
        {
            Code = code;
            Message = message;
            Description = description;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Error))
            {
                return false;
            }
            return (Code == ((Error)obj).Code
                && Description == ((Error)obj).Description
                && Message == ((Error)obj).Message);
        }

        public bool IsPermissionDeniedError()
        {
            return Equals(Error.PermissionDeniedError());
        }

        public static Error InvalidRequestError(string description)
        {
            return new Error()
            {
                Code = ErrorConstants.InvalidRequestInputCode,
                Message = ErrorConstants.InvalidRequestInputMessage,
                Description = description
            };
        }

        public static Error InvalidUserRequestError(string description)
        {
            return new Error()
            {
                Code = ErrorConstants.InvalidUserRequestInputCode,
                Message = ErrorConstants.InvalidUserRequestInputMessage,
                Description = description
            };
        }

        public static Error InvalidRequestError(int code, string description)
        {
            return new Error()
            {
                Code = code,
                Message = GetErrorMessage(code),
                Description = description
            };
        }

        public static Error UnauthorizedAccessError()
        {
            return new Error()
            {
                Code = ErrorConstants.UnauthorizedAccess,
                Message = ErrorConstants.UnauthorizedAccessMessage,
                Description = ErrorConstants.UnauthorizedAccessDescription
            };
        }

        public static Error PermissionDeniedError()
        {
            return new Error()
            {
                Code = ErrorConstants.PermissionDeniedCode,
                Message = ErrorConstants.PermissionDeniedMessage,
                Description = ErrorConstants.PermissionDeniedDescription
            };
        }

        private static string GetErrorMessage(int code)
        {
            var message = code switch
            {
                ErrorConstants.PermissionDeniedCode => ErrorConstants.PermissionDeniedMessage,
                ErrorConstants.InvalidRequestInputCode => ErrorConstants.InvalidRequestInputMessage,
                ErrorConstants.UnhandledErrorCode => ErrorConstants.UnhandledErrorCodeMessage,
                ErrorConstants.UnauthorizedAccess => ErrorConstants.UnauthorizedAccessMessage,
                _ => ErrorConstants.InvalidRequestInputMessage,
            };
            return message;
        }
    }

    public class ApiResponse
    {
        public IList<Error> Errors { get; set; }
    }
}
