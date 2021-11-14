using System;
using Microsoft.AspNetCore.Identity;

namespace Auth.Persistence.Models
{
    public class Role : IdentityRole<Guid>
    {
        private Role()
        {
            
        }

        public Role(string name): base(name)
        {
        }
    }
}