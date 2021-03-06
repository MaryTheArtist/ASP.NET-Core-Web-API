using Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(7),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256 )
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);

            if (userExists != null)
                return StatusCode(500, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.FullName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
                return StatusCode(500, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again!" });

            return Ok(new Response { Status = "Success", Message = "User created successfully"}) ;
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExist != null)
                return StatusCode(500, new Response { Status = "Error", Message = "User already exists!" });
            
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.FullName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
                return StatusCode(500, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            //if (!await roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-superadmin")]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] RegisterModel registerModel)
        {
            var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExist != null)
                return StatusCode(500, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.FullName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
                return StatusCode(500, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));

            if (await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

    }
}
