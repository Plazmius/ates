using System.Collections.Generic;

namespace Auth.Persistence.Models
{
    public static class Roles
    {
        public const string Admin = "admin";
        public const string Manager = "manager";
        public const string Worker = "worker";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Admin;
                yield return Manager;
                yield return Worker;
            }
        }
    }
}