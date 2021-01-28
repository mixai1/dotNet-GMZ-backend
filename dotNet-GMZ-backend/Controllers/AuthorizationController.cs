using dotNet_GMZ_backend.Models.IdentityModels;
using dotNet_GMZ_backend.Models.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace dotNet_GMZ_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<RoleApp> _roleManager;
        private readonly SignInManager<UserApp> _signInManager;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(UserManager<UserApp> userManager, SignInManager<UserApp> signInManager,
            RoleManager<RoleApp> roleManager, ILogger<AuthorizationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [Route("register")]
        [HttpPost]
        //POST : /api/Authorization/register
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                _logger.LogInformation(nameof(Register));
                if (TryValidateModel(userRegisterDto))
                {
                    var newUser = new UserApp()
                    {
                        Email = userRegisterDto.Email,
                        UserName = userRegisterDto.UserName
                    };

                    var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

                    if (userRegisterDto.UserName == "Super_Admin")
                    {
                        await _roleManager.CreateAsync(new RoleApp() { Name = "Admin" });
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new RoleApp() { Name = "User" });
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
                _logger.LogError(e, nameof(Register));
                return BadRequest("Error!");
            }
        }

        [Route("login")]
        [HttpPost]
        //POST : /api/Authorization/login
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                _logger.LogInformation(nameof(Login));
                if (TryValidateModel(userLoginDto))
                {
                    var findUser = await _userManager.FindByNameAsync(userLoginDto.UserName);
                    if (findUser != null &&
                        await _userManager.CheckPasswordAsync(findUser, userLoginDto.Password))
                    {
                        var token = CreateToken(findUser);
                        return Ok(token);
                    }
                    _logger.LogError(nameof(Login));
                    return BadRequest("Error");
                }
                _logger.LogError(nameof(Login));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(Login));
                return BadRequest("Error");
            }
        }

        private string CreateToken(UserApp userApp)
        {
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userApp.Id.ToString() ),
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("ReallySecretKey")),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}