using Microsoft.AspNetCore.Identity;

namespace Auth.Persistence.Models
{
    public class Role : IdentityRole<ulong>
    {
        private Role()
        {
            
        }

        public Role(string name): base(name)
        {
        }
    }
}