using System.Collections.Generic;
using System.Security.Claims;
using Auth.Persistence.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ated_id.Services
{
    public class PopugClaimsService : DefaultClaimsService
    {
        private readonly UserManager<Popug> _userManager;

        public PopugClaimsService(IProfileService profile, ILogger<DefaultClaimsService> logger,
            UserManager<Popug> userManager) : base(profile, logger)
        {
            _userManager = userManager;
        }

        protected override IEnumerable<Claim> GetStandardSubjectClaims(ClaimsPrincipal subject)
        {
            var sub = subject.GetSubjectId();
            var user = _userManager.FindByIdAsync(sub).GetAwaiter().GetResult();
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user?.Id.ToString() ?? sub),
                new Claim(JwtClaimTypes.AuthenticationTime, subject.GetAuthenticationTimeEpoch().ToString(),
                    ClaimValueTypes.Integer),
                new Claim(JwtClaimTypes.IdentityProvider, subject.GetIdentityProvider())
            };

            claims.AddRange(subject.GetAuthenticationMethods());

            return claims;
        }
    }
}