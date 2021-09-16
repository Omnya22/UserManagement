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
    }
}
