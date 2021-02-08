using dotNet_GMZ_backend.Models.AppSettingsModels;
using dotNet_GMZ_backend.Models.IdentityModels;
using dotNet_GMZ_backend.Models.ModelsDTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using AutoMapper;
using dotNet_GMZ_backend.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;

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
        private readonly AppSettings _optionsApp;
        private readonly IMapper _mapper;

        public AuthorizationController(UserManager<UserApp> userManager, SignInManager<UserApp> signInManager,
            RoleManager<RoleApp> roleManager, ILogger<AuthorizationController> logger,
            IOptions<AppSettings> optionsApp, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _optionsApp = optionsApp.Value;
        }

        [Route("register")]
        [HttpPost]
        //POST : /api/Authorization/register
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                _logger.LogInformation(nameof(AuthorizationController.Register));
                if (TryValidateModel(userRegisterDto))
                {
                    var newUser = _mapper.Map<UserApp>(userRegisterDto);

                    var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }

                    if (userRegisterDto.UserName == "Super_Admin")
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new RoleApp() { Name = "Admin" });
                        }
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new RoleApp() { Name = "User" });
                        }

                        await _userManager.AddToRoleAsync(newUser, "User");
                    }

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, false);
                        return Ok(result);
                    }
                }
                _logger.LogError(nameof(AuthorizationController.Register));
                return BadRequest("Error!");
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(AuthorizationController.Register), e);
                return BadRequest(e.Message);
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
                        var token = CreateToken(findUser).Result;
                        return Ok(new {token});
                    }
                    _logger.LogError(nameof(AuthorizationController.Login));
                    return BadRequest("Error");
                }
                _logger.LogError(nameof(AuthorizationController.Login));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(AuthorizationController.Login), e);
                return BadRequest(e.Message);
            }
        }

        private async Task<string> CreateToken(UserApp userApp)
        {
            var role = await _userManager.GetRolesAsync(userApp);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userApp.Id.ToString() ),
                    new Claim(new IdentityOptions().ClaimsIdentity.RoleClaimType,role.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_optionsApp.JWT_Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}