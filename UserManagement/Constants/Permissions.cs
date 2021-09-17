using System;
using System.Collections.Generic;

namespace UserManagement.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissions(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            }; 
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissions(module.ToString()));

            return allPermissions;
        }
    }
}
