using System.Threading.Tasks;
using System.Web.Http;
using ChatTest.Interfaces;
using ChatTest.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace ChatTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }


        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IActionResult> Login([System.Web.Http.FromBody] LoginIncomeModel model)
        {
            return await _securityService.Login(model);
        }
    }
}
