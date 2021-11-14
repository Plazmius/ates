using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Persistence.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ated_id.Services
{
    public class IdentityProfileService : IProfileService
    {
        private readonly UserManager<Popug> _userManager;
        private RoleManager<Role> _roleManager;
        private IUserClaimsPrincipalFactory<Popug> _claimsPrincipalFactory;

        public IdentityProfileService(UserManager<Popug> userManager, RoleManager<Role> roleManager,
            IUserClaimsPrincipalFactory<Popug> claimsPrincipalFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }
        
        

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var Id = Guid.Parse(sub);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == Id);
            var principal = await _claimsPrincipalFactory.CreateAsync(user);
            var claims = principal.Claims
                .Where(c => context.RequestedClaimTypes.Contains(c.Type))
                .ToList();

            if (_userManager.SupportsUserRole)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (!_roleManager.SupportsRoleClaims) continue;
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        claims.AddRange(await _roleManager.GetClaimsAsync(role));
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}