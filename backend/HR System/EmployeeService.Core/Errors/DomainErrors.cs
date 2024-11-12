using EmployeeService.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static class DomainErrors
    {
        /// <summary>
        /// Contains the user errors.
        /// </summary>
        public static class User
        {
            public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");

            public static Error InvalidPermissions => new Error(
                "User.InvalidPermissions",
                "The current user does not have the permissions to perform that operation.");

            public static Error DuplicateEmail => new Error("User.DuplicateEmail", "The specified email is already in use.");

            public static Error CannotChangePassword => new Error(
                "User.CannotChangePassword",
                "The password cannot be changed to the specified password.");
        }

        public static class Employee
        {
            public static Error NotFound => new Error("Employee.NotFound", "The Employee with the specified identifier was not found.");
            public static Error DuplicateId => new Error("Employee.DuplicateId", "The specified Id is already in use.");
            public static Error AlreadyExists => new Error("Employee.AlreadyExists", "The provided employee already exists");
        }

    }
}
