using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dotNet_GMZ_backend.DAL;
using dotNet_GMZ_backend.Models.IdentityModels;
using dotNet_GMZ_backend.Models.ModelsDTO;
using Microsoft.AspNetCore.Identity;

namespace dotNet_GMZ_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<RoleApp> _roleManager;
        private readonly SignInManager<UserApp> _signInManager;

        public AuthorizationController(UserManager<UserApp> userManager,SignInManager<UserApp> signInManager, RoleManager<RoleApp> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Route("register")]
        [HttpPost]
        //POST : /api/Authorization/register
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            try
            {
                if (TryValidateModel(model))
                {
                    var newUser = new UserApp() { Email = model.Email, UserName = model.UserName };

                    var result = await _userManager.CreateAsync(newUser, model.Password);

                    if (model.UserName == "Super_Admin")
                    {
                        await _roleManager.CreateAsync(new RoleApp() {Name = "Admin"});
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new RoleApp(){Name = "User"});
                        await _userManager.AddToRoleAsync(newUser, "User");
                    }

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, false);
                        return Ok(result);
                    }

                }
                return BadRequest("Error!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error!");
            }
        }
    }
}
