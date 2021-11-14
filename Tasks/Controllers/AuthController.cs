using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: Controller
    {
        public ActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties() 
                { 
                    RedirectUri = returnUrl
                }
            );
        }
    }
}