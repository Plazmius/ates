using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ated_id.Services
{
    public sealed class PopugClaimsFactory : UserClaimsPrincipalFactory<Popug, Role>
    {
        public PopugClaimsFactory(UserManager<Popug> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {}   

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Popug user)
        { 
            var identity = await base.GenerateClaimsAsync(user);
            var sub = identity.FindFirst("sub");
            if (sub == null)
                return identity; 

            identity.RemoveClaim(sub);
            identity.AddClaim(new Claim("sub", user.Id.ToString()));

            return identity;
        }
    }
}