using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [Route("/register")]
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            return null;
        }
    }
}
