using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Models.Constant
{
    public class ErrorConstants
    {
        public const int UnhandledErrorCode = 10003;
        public const string UnhandledErrorCodeDescription = "Unhandled error occured while processing request.";
        public const string UnhandledErrorCodeMessage = "Unhandled error occured while processing request.";


        public const int PermissionDeniedCode = 1004;
        public const string PermissionDeniedMessage = "Permission denied";
        public const string PermissionDeniedDescription = "Either the resource does not exist or you do not have permissions to it.";

        public const int InvalidRequestInputCode = 1005;
        public const string InvalidRequestInputMessage = "Request inputs are invalid";

        public const int InvalidUserRequestInputCode = 1007;
        public const string InvalidUserRequestInputMessage = "Invalid User";

        public const int UniquekeyViolationCode = 1006;
        public const string UniquekeyViolationCodeMessage = "Duplicate record already exist. Unique key violation";
        public const string UniquekeyViolationCodeDescription = "Duplicate record already exist. Unique key violation";

        public const int InSufficientPermissions = 1009;
        public const string InSufficientPermissionsMessage = "User has insufficient Permissions";
        public const string InSufficientPermissionsDescription = "The user does not have access to the requested resource";

        public const int UnauthorizedAccess = 1010;
        public const string UnauthorizedAccessMessage = "User has Unauthorized Access";
        public const string UnauthorizedAccessDescription = "The user is not authorized for this action or resource";
    }
}
